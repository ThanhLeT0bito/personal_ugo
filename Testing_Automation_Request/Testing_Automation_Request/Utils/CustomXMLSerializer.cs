using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Reflection;
using StringWriter = System.IO.StringWriter;
using Newtonsoft.Json;
using static CloudBanking.Utilities.CustomFormat.CustomXMLSerializer;
using System.Data.SqlTypes;
using static System.Net.Mime.MediaTypeNames;

namespace CloudBanking.Utilities.CustomFormat
{
    /// <summary>
    ///  XMLObjectSerializer class Handles the Deserializing and Serializing of XML data
    /// </summary>
    /// 

    public class CustomXMLSerializer : ICustomSerializer
    {
        private XmlSerializer xmlSerializer;

        public void SetAssemblies(Assembly[] assemblies) { }

        public string SerializeToString(object o)
        {

            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            Type objtype = o.GetType();

            var serializer = new XmlSerializer(objtype);

            var settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                try
                {
                    serializer.Serialize(writer, o, emptyNamepsaces);
                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex.ToString());
                }

                return stream.ToString();
            }
        }

        public void SerializeToStream(object o, Stream stream)
        {

            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            Type objtype = o.GetType();

            var serializer = new XmlSerializer(objtype);

            var settings = new XmlWriterSettings();

            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);

            using (var writer = XmlWriter.Create(streamWriter, settings))
            {
                serializer.Serialize(writer, o, emptyNamepsaces);

                streamWriter.Flush();
            }
        }

        public object DeserializeFromStream(Stream s, Type type)
        {
            xmlSerializer = new XmlSerializer(type);

            var reader = new StreamReader(s, Encoding.UTF8, true);
            {
                return xmlSerializer.Deserialize(reader);
            }
        }

        public object DeserializeFromString(string s, Type type)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            object obj = null;

            xmlSerializer = new XmlSerializer(type);

            using (MemoryStream memStream = new MemoryStream(Encoding.Unicode.GetBytes(s)))
            {
                obj = xmlSerializer.Deserialize(memStream);
            }

            return obj;
        }

        public static string Serialize(object obj)
        {
            if (obj == null) return string.Empty;

            XmlSerializer xsSubmit = new XmlSerializer(obj.GetType());
            using (var sww = new StringWriter())
            {
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);

                xsSubmit.Serialize(sww, obj, xns);

                var doc = new XmlDocument();
                doc.LoadXml(sww.ToString());

                if (doc.FirstChild is XmlDeclaration xmlDeclaration)
                {
                    doc.RemoveChild(xmlDeclaration);
                }

                return Beautify(doc);
            }
        }

        private static string Beautify(XmlDocument doc)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (XmlTextWriter xtw = new XmlTextWriter(sw))
                {
                    xtw.Formatting = System.Xml.Formatting.Indented;
                    xtw.Indentation = 4;
                    doc.WriteTo(xtw);
                }
                return sw.ToString();
            }
        }

        public static T Deserialize<T>(string text)
        {
            return (T)Deserialize(text, typeof(T));
        }   
        
        public static object Deserialize(string text,Type type)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            var attr = type.GetCustomAttribute<XmlRootAttribute>();
            if (attr == null)
            {
                attr = new XmlRootAttribute()
                {
                    Namespace = ""
                };
            }

            XmlSerializer serializer = new XmlSerializer(type, attr);

            using (TextReader reader = new StringReader(text))
            {
                return serializer.Deserialize(reader);
            }
        }
    }
}
