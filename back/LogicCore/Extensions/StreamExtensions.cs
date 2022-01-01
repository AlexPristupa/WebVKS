using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogicCore.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadAllBytes(this Stream stream, int bufferSize = 8 * 1024)
        {
            if (stream.CanSeek)
            {
                stream.Position = 0;
                var data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return data;
            }
            byte[] buffer = new byte[bufferSize];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
