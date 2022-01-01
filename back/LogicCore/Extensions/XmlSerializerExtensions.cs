using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LogicCore.Extensions
{
    /// <summary>
    /// Типизированный сериализатор (надстройка над XmlSerializer)
    /// </summary>
    public static class XmlSerializerExtensions
    {
        /// <summary>
        /// Восстановить данные из потока
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="stream">Поток сериализованных данных</param>
        public static T Deserialize<T>(this XmlSerializer serializer, Stream stream)
        {
            return (T)serializer.Deserialize(stream);
        }

        /// <summary>
        /// Сериализовать данные в поток (без дополнительных пространств имен в заголовке xml)
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="stream">Поток для сериализованных данных</param>
        /// <param name="data">Данные для сериализации</param>
        public static void SerializeWithoutNamespace(this XmlSerializer serializer, Stream stream, object data)
        {
            serializer.Serialize(stream, data,
                                 new XmlSerializerNamespaces(new[] { new XmlQualifiedName() }));
        }

    }


    /// <summary>
    /// Типизированный сериализатор (надстройка над XmlSerializer)
    /// </summary>
    public static class XmlSerializer<T>
    {
        private static XmlSerializer _serializer;

        public static XmlSerializer Serializer
        {
            get { return _serializer ?? (_serializer = new XmlSerializer(typeof(T))); }
        }

        /// <summary>
        /// Восстановить данные из потока
        /// </summary>
        /// <param name="stream">Поток сериализованных данных</param>
        public static T Deserialize(Stream stream)
        {
            return (T)Serializer.Deserialize(stream);
        }

        /// <summary>
        /// Восстановить данные из потока
        /// </summary>
        /// <param name="stream">Поток сериализованных данных</param>
        public static T Deserialize(byte[] data)
        {
            return (T)Serializer.Deserialize(new MemoryStream(data));
        }

        /// <summary>
        /// Восстановить данные из потока
        /// </summary>
        /// <param name="reader">Поток сериализованных данных</param>
        public static T Deserialize(TextReader reader)
        {
            return (T)Serializer.Deserialize(reader);
        }
        /// <summary>
        /// Восстановить данные из потока
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        public static T Deserialize(string fileName)
        {
            using (var reader = new FileStream(fileName, FileMode.Open))
            {
                return (T)Serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Попытаться загрузить данные из файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        public static T TryDeserialize(string fileName)
        {
            var result = default(T);
            try
            {
                if (File.Exists(fileName))
                {
                    result = Deserialize(fileName);
                }
            }
            catch (System.Exception)
            {
                //ничего не делаем
            }
            return result;
        }

        /// <summary>
        /// Сериализовать данные в поток
        /// </summary>
        /// <param name="stream">Поток для сериализованных данных</param>
        /// <param name="data">Данные для сериализации</param>
        public static void Serialize(Stream stream, T data)
        {
            Serializer.Serialize(stream, data);
        }

        /// <summary>
        /// Сериализовать в строку
        /// </summary>
        public static string Serialize(T data, bool indent = false)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = indent;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                Serializer.Serialize(writer, data, emptyNamespaces);
                return stream.ToString();
            }
        }

        /// <summary>
        /// Восстановить из строки
        /// </summary>
        public static T DeserializeFromString(string data)
        {
            using (var reader = new StringReader(data))
            {
                return (T)Serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Сериализовать данные в файл
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="data">Данные для сериализации</param>
        /// <param name="useNamespace">Создавать просранство имен в заголовке файла</param>
        public static void Serialize(string fileName, T data, bool useNamespace)
        {
            var settings = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
            using (var writer = XmlWriter.Create(fileName, settings))// Encoding.UTF8))
            {
                if (useNamespace)
                {
                    Serializer.Serialize(writer, data);
                }
                else
                {
                    Serializer.Serialize(writer, data,
                                         new XmlSerializerNamespaces(new[] { new XmlQualifiedName() }));
                }
            }
        }



        /// <summary>
        /// Сериализовать данные в поток (без дополнительных пространств имен в заголовке xml)
        /// </summary>
        /// <param name="stream">Поток для сериализованных данных</param>
        /// <param name="data">Данные для сериализации</param>
        public static void SerializeWithoutNamespace(Stream stream, object data)
        {
            Serializer.Serialize(stream, data,
                                 new XmlSerializerNamespaces(new[] { new XmlQualifiedName() }));
        }

        /// <summary>
        /// Сериализовать данные в поток (без заголовка вообще)
        /// </summary>
        /// <param name="stream">Поток для сериализованных данных</param>
        /// <param name="data">Данные для сериализации</param>
        public static void SerializeWithoutHeader(Stream stream, object data)
        {
            Serializer.Serialize(stream, data,
                                 new XmlSerializerNamespaces(new[] { new XmlQualifiedName() }));
        }

        /// <summary>
        /// Сериализовать данные в поток (без дополнительных пространств имен в заголовке xml)
        /// </summary>
        /// <param name="stream">Поток для сериализованных данных</param>
        /// <param name="data">Данные для сериализации</param>
        public static void SerializeWithoutNamespace(XmlWriter stream, object data)
        {
            Serializer.Serialize(stream, data,
                                 new XmlSerializerNamespaces(new[] { new XmlQualifiedName() }));
        }
        public static bool Check(T data)
        {
            try
            {
                var stream = new MemoryStream();
                Serializer.Serialize(stream, data);
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
    }
}
