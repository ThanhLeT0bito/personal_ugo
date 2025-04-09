using System;
using System.IO;
using System.Reflection;

namespace CloudBanking.Utilities.CustomFormat
{
	public enum DataFormat
	{
		XML = 1,
		JSON
	}

	public static class CustomSerializerFormat
	{
		public static string FormatToString(DataFormat format)
		{
			switch (format)
			{
				case DataFormat.XML:

					return "XML";

				case DataFormat.JSON:

					return "JSON";
			}
			return "";
		}

		public static ICustomSerializer FormatSerializer(int format)
		{
			return FormatSerializer(format, null);
		}

		public static ICustomSerializer FormatSerializer(int format, Assembly[] assemblies)
		{

			switch (format)
			{
				case (int)DataFormat.XML:

					return new CustomXMLSerializer();

				case (int)DataFormat.JSON:

					return new CustomJSONSerializer(assemblies);

			}
			return null;
		}

		public static ICustomSerializer FormatSerializer(string format)
		{
			switch (format)
			{
				case "XML":

					return new CustomXMLSerializer();

				case "JSON":

					return new CustomJSONSerializer();
			}

			return null;
		}
	}

    public interface ICustomSerializer
    {
        void SetAssemblies(Assembly[] assemblies);

        string SerializeToString(object o);

        void SerializeToStream(object o, Stream stream);

        object DeserializeFromStream(Stream s, Type type);

        object DeserializeFromString(string s, Type type);
    }
}
