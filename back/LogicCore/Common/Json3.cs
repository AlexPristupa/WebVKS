using LogicCore.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace LogicCore
{
    public static class Json3
    {
        public static string ToString(object item, JsonConverter converter = null)
        {
            var serializer = new JsonSerializerOptions();
            serializer.WriteIndented = true;
            //serializer.Culture = CultureInfo.InvariantCulture;
            serializer.MaxDepth = 10;
            if (converter != null)
                serializer.Converters.Add(converter);

            var result = JsonSerializer.Serialize(item, item.GetType(), serializer);
            return result;
        }

        public static T FromString<T>(string data, JsonConverter converter = null)
        {
            var serializer = new JsonSerializerOptions();
            if (converter != null)
                serializer.Converters.Add(converter);
            var result = JsonSerializer.Deserialize<T>(data, serializer);
            return (T)result;
        }

        public static void Save(object item, string path)
        {
            var serializer = new JsonSerializerOptions();
            serializer.WriteIndented = true;
            serializer.IgnoreNullValues = true;

            using var writer = File.Create(path);
            using (var uwriter = new Utf8JsonWriter(writer))
            {
                JsonSerializer.Serialize(uwriter, item, item.GetType(), serializer);
            }
        }

        public static T Load<T>(string path)
        {
            using (var reader = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                var span = new ReadOnlySpan<byte>(reader.ReadAllBytes());
                var result = JsonSerializer.Deserialize<T>(span);
                return result;
            }
        }

    }
}