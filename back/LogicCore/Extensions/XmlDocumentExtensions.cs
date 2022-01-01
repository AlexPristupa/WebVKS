using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace LogicCore.Extensions
{
    public static class XmlDocumentExtensions
    {
        public static XmlNode FindElementByTagName(this XmlDocument xml, string name)
        {
            var nodes = xml.GetElementsByTagName(name);
            if (nodes.Count > 0)
            {
                return nodes[0];
            }
            return null;
        }
    }
}
