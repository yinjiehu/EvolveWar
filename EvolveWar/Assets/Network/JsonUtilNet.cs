using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;

namespace Network.Data
{
    public class JsonUtilNet
    {
        static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            Formatting = Formatting.Indented,
            ContractResolver = new JsonConditionIgnoreContractResolver<JsonIgnoreWhenInternet>(),
            Binder = new JsonInterfaceBinder(),
            Converters = new List<JsonConverter>()
            {
                new CustomDateTimeConverter(),
			}
        };

        static JsonSerializer _serializer = new JsonSerializer()
		{
			TypeNameHandling = TypeNameHandling.Objects,
			TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            ContractResolver = new JsonConditionIgnoreContractResolver<JsonIgnoreWhenInternet>(),
            Binder = new JsonInterfaceBinder()
        };
		static JsonUtilNet()
		{
			_serializer.Converters.Add(new CustomDateTimeConverter());
		}

		public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }

		public static T Deserialize<T>(string str)
        {
			return JsonConvert.DeserializeObject<T>(str, _settings);
        }

        public static object Deserialize(string str, Type type)
        {
            return JsonConvert.DeserializeObject(str, type, _settings);
		}

		public static byte[] SerializeToBson(object obj)
		{
			MemoryStream ms = new MemoryStream();
			using (BsonWriter writer = new BsonWriter(ms))
			{
				_serializer.Serialize(writer, obj);
			}
			return ms.ToArray();
		}
		public static T DeserializeFromBson<T>(byte[] bytes)
		{
			T ret = default(T);
			MemoryStream ms = new MemoryStream();
			using (BsonReader reader = new BsonReader(ms))
			{
				ms.Write(bytes, 0, bytes.Length);
				ret = _serializer.Deserialize<T>(reader);
			}
			return ret;
		}

		public static object DeserializeFromBson(byte[] bytes, Type type)
		{
			object ret;
			MemoryStream ms = new MemoryStream();
			using (BsonReader reader = new BsonReader(ms))
			{
				ms.Write(bytes, 0, bytes.Length);
				ret = _serializer.Deserialize(reader, type);
			}
			return ret;
		}
		public static object DeserializeFromBson(string str, Type type)
		{
			return DeserializeFromBson(Convert.FromBase64String(str), type);
		}
	}
}