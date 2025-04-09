using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace CloudBanking.Utilities.CustomFormat
{
    public class SingleAssemblyJsonTypeBinder : DefaultSerializationBinder
    {
        private Dictionary<string, Type> _typesBySimpleName = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<Type, string> _simpleNameByType = new Dictionary<Type, string>();

        public SingleAssemblyJsonTypeBinder(Assembly[] assemblies)
        {
            _typesBySimpleName = new Dictionary<string, Type>();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.ExportedTypes)
                {
                    if (_typesBySimpleName.ContainsKey(type.FullName))

                        throw new InvalidOperationException("Cannot user PolymorphicBinder on a namespace where multiple public types have same name.");

                    _typesBySimpleName[type.FullName] = type;
                    _simpleNameByType[type] = type.FullName;
                }
            }
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            Type result;
            if (_typesBySimpleName.TryGetValue(typeName.Trim(), out result))
                return result;

            return null;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            string name;

            if (_simpleNameByType.TryGetValue(serializedType, out name))
            {
                typeName = name;
                assemblyName = null;// _assembly.FullName;
            }
            else
            {
                typeName = null;
                assemblyName = null;
            }
        }
    }
    public class CustomJSONSerializer : ICustomSerializer
    {
        private Assembly[] assemblies;

        public CustomJSONSerializer()
        {
        }

        public CustomJSONSerializer(Assembly[] assemblies)
        {
            this.assemblies = assemblies;
        }

        public void SetAssemblies(Assembly[] assemblies)
        {
            this.assemblies = assemblies;
        }

        static string StreamToString(Stream stream, Encoding encoding)
        {
            stream.Position = 0;

            using (StreamReader reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        public string SerializeToString(object o)
        {
            JsonSerializerSettings settings     = new JsonSerializerSettings();

            settings.TypeNameHandling           = TypeNameHandling.Objects;
            settings.NullValueHandling          = NullValueHandling.Ignore;

            settings.ReferenceLoopHandling      = ReferenceLoopHandling.Serialize;

            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            if (assemblies != null)

                settings.SerializationBinder = new SingleAssemblyJsonTypeBinder(assemblies);

            return JsonConvert.SerializeObject(o, settings);
        }

        public void SerializeToStream(object o, Stream stream)
        {

            var serializer                          = new JsonSerializer();

            serializer.TypeNameHandling             = TypeNameHandling.Objects ;
            serializer.NullValueHandling            = NullValueHandling.Ignore;

            serializer.ReferenceLoopHandling        = ReferenceLoopHandling.Serialize;

            serializer.PreserveReferencesHandling   = PreserveReferencesHandling.Objects;

            if (assemblies != null)

                serializer.SerializationBinder = new SingleAssemblyJsonTypeBinder(assemblies);

            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);

            var writer = new JsonTextWriter(streamWriter);
            {
                serializer.Serialize(writer, o);

                streamWriter.Flush();
            }
        }

        public object DeserializeFromStream(Stream s, Type type)
        {
            var serializer                          = new JsonSerializer();

            serializer.TypeNameHandling             = TypeNameHandling.Objects;
            serializer.NullValueHandling            = NullValueHandling.Ignore;

            serializer.ReferenceLoopHandling        = ReferenceLoopHandling.Serialize;

            serializer.PreserveReferencesHandling   = PreserveReferencesHandling.Objects;

            if (assemblies != null)

                serializer.SerializationBinder = new SingleAssemblyJsonTypeBinder(assemblies);

            var streamreader = new StreamReader(s);

            var reader = new JsonTextReader(streamreader);
            {
                return serializer.Deserialize(reader, type);
            }

        }

        public object DeserializeFromString(string s, Type type)
        {

            JsonSerializerSettings settings     = new JsonSerializerSettings();

            settings.TypeNameHandling           = TypeNameHandling.Objects;
            settings.NullValueHandling          = NullValueHandling.Ignore;

            settings.ReferenceLoopHandling      = ReferenceLoopHandling.Serialize;

            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            if (assemblies != null)

                settings.SerializationBinder = new SingleAssemblyJsonTypeBinder(assemblies);

            if (s != null)

                return JsonConvert.DeserializeObject(s, settings);

            return null;
        }
    }
}
