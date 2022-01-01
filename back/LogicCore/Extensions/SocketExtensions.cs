using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicCore.Extensions
{
    public static class SocketExtensions
    {

        public static async Task<int> SendToAsync(this Socket socket,
            byte[] buffers,
            SocketFlags socketFlags, 
            EndPoint ep,
            TimeSpan timeout,
            CancellationToken cancellation)
        {
            var completionSource = new TaskCompletionSource<int>(socket);

            using var timeCancellation = new CancellationTokenSource(timeout);
            using (cancellation.Register(
                s => ((TaskCompletionSource<SocketReceiveMessageFromResult>)s).TrySetCanceled(),
                        completionSource))
            using (timeCancellation.Token.Register(
                s => ((TaskCompletionSource<SocketReceiveMessageFromResult>)s).TrySetException(new TimeoutException()),
                completionSource))
            {
                socket.BeginSendTo(buffers, 0, buffers.Length, socketFlags, ep, iar =>
                {
                    var asyncState = (TaskCompletionSource<int>)iar.AsyncState;
                    try
                    {
                        asyncState.TrySetResult(((Socket)asyncState.Task.AsyncState).EndSend(iar));
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                    catch (Exception ex)
                    {
                        asyncState.TrySetException(ex);
                    }
                }, completionSource);
                return await completionSource.Task;
            }
        }

        public static async Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(
           this Socket socket,
           ArraySegment<byte> buffer,
           SocketFlags socketFlags,
           EndPoint remoteEndPoint,
           TimeSpan timeout,
           CancellationToken cancellation)
        {
            var completionSource = new StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult>(socket);
            completionSource._field1 = socketFlags;
            completionSource._field2 = remoteEndPoint;

            using var timeCancellation = new CancellationTokenSource(timeout);
            using (cancellation.Register(
                s => ((TaskCompletionSource<SocketReceiveMessageFromResult>)s).TrySetCanceled(),
                completionSource))
            using (timeCancellation.Token.Register(
                s => ((TaskCompletionSource<SocketReceiveMessageFromResult>)s).TrySetException(new TimeoutException()),
                completionSource))
            {
                socket.Bind(remoteEndPoint);
                socket.BeginReceiveMessageFrom(buffer.Array, buffer.Offset, buffer.Count, socketFlags, ref completionSource._field2, (iar =>
                {
                    var asyncState = (StateTaskCompletionSource<SocketFlags, EndPoint, SocketReceiveMessageFromResult>)iar.AsyncState;
                    try
                    {
                        IPPacketInformation ipPacketInformation;
                        int messageFrom = ((Socket)asyncState.Task.AsyncState)
                                            .EndReceiveMessageFrom(iar, ref asyncState._field1, ref asyncState._field2, out ipPacketInformation);
                        asyncState.TrySetResult(new SocketReceiveMessageFromResult()
                        {
                            ReceivedBytes = messageFrom,
                            RemoteEndPoint = asyncState._field2,
                            SocketFlags = asyncState._field1,
                            PacketInformation = ipPacketInformation
                        });
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                    catch (Exception ex)
                    {
                        asyncState.TrySetException(ex);
                    }
                }), completionSource);
                return await completionSource.Task;
            }
        }
    }

    class StateTaskCompletionSource<TField1, TField2, TResult> : TaskCompletionSource<TResult>
    {
        internal TField1 _field1;
        internal TField2 _field2;

        public StateTaskCompletionSource(object baseState)
          : base(baseState)
        {
        }

        public StateTaskCompletionSource(object baseState, TaskCreationOptions options)
          : base(baseState, options)
        {
        }
    }
}
