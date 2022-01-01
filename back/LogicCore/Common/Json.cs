using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicCore
{
    public class Json
    {
        public static string ToString(object item, JsonConverter converter = null, bool skipNull = false)
        {
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.Culture = CultureInfo.InvariantCulture;
            serializer.MaxDepth = 10;
            if (skipNull)
                serializer.NullValueHandling = NullValueHandling.Ignore;
            if (converter != null)
                serializer.Converters.Add(converter);
            //serializer.NullValueHandling = NullValueHandling.Ignore;
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, item);
                }
            }
            return sb.ToString();
        }

        public static T FromString<T>(string data, JsonConverter converter = null)
        {
            var serializer = new JsonSerializer();
            if (converter != null)
                serializer.Converters.Add(converter);
            using (var reader = new StringReader(data))
            {
                var result = serializer.Deserialize(reader, typeof(T));
                return (T)result;
            }
        }

        public static void Save(object item, string path)
        {
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            //Newtonsoft.Json.JsonConvert.SerializeObject()
            using (var writer = File.CreateText(path))
            {
                 serializer.Serialize(writer, item);
            }
        }

        public static void Save(object item, Stream stream)
        {
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            var writer = new StreamWriter(stream);
            serializer.Serialize(writer, item);
            writer.Flush();
        }

        public static T Load<T>(string path)
        {
            return Load<T>(path, Encoding.UTF8);
        }

        public static T Load<T>(string path, Encoding enc)
        {
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(path, enc))
            {
                var result = serializer.Deserialize(reader, typeof(T));
                return (T)result;
            }
        }

        public static T Load<T>(TextReader reader)
        {
            var serializer = new JsonSerializer();
            var result = serializer.Deserialize(reader, typeof(T));
            return (T)result;
        }

        public static T Load<T>(Stream stream)
        {
            var serializer = new JsonSerializer();
            using var reader = new StreamReader(stream, null, true, -1, true);
            var result = serializer.Deserialize(reader, typeof(T));
            return (T)result;
        }

        public static T LoadFromNetwork<T>(NetworkStream stream, ILogger logger) where T : class
        {
            var serializer = new JsonSerializer();
            //var memory = new MemoryStream();
            //var buffer = new byte[2048];
            //do
            //{
                //var read = stream.Read(buffer, 0, buffer.Length);
                //if (read == 0 && logger.IsTraceEnabled)
                //{
                //    logger.Trace("Получен пустой пакет");
                //}
                //memory.Write(buffer, 0, read);
                //memory.Position = 0;
                //if (memory.Length > 0)
                {
                    using var reader = new StreamReader(new StreamTcpReader(stream, logger), null, true, -1, true);
                    var result = serializer.Deserialize(reader, typeof(T));
                    return (T)result;
                }
                //Thread.Sleep(waitReadBlock);
            //} while (stream.DataAvailable);
            //return null;
        }

        public static int SendToNetwork(object item, NetworkStream stream, ILogger logger)
        {
            var serializer = new JsonSerializer();
            if (logger.IsTraceEnabled)
                serializer.Formatting = Formatting.Indented;
            serializer.NullValueHandling = NullValueHandling.Ignore;

            var memory = new MemoryStream();
            var writer = new StreamWriter(memory);
            serializer.Serialize(writer, item);
            writer.Flush();
            stream.Write(memory.GetBuffer(), 0, (int)memory.Length);
            stream.Flush();
            return (int)memory.Length;
        }


    }

    class StreamTcpReader : Stream
    {
        private readonly NetworkStream _stream;
        private readonly ILogger _logger;
        private int _count;
        private bool _eof;

        public StreamTcpReader(NetworkStream stream, ILogger logger)
        {
            _stream = stream;
            _logger = logger;
        }

        public int BufferSize { get; set; }
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => _stream.Length;
        public override long Position { get => _stream.Position; set => _stream.Position = value; }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_eof) return 0;
            var result = _stream.Read(buffer, offset, count);
            if (_logger.IsTraceEnabled)
            {
                _count += result;
                _logger.Trace($"Приняли: {result} байт (всего: {_count})");
            }
            //анализируем конец файла
            if (result == 0 ||
                _stream.DataAvailable == false &&
                buffer[offset + result - 1] == '}')
            {
                _eof = true;
            }
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }
}
