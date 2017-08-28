using IronFury.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;

namespace Kit.Json
{
    public class JsonUtilDB
    {
		static JsonSerializerSettings _settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Objects,
			TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
			Formatting = Formatting.Indented,
			DateTimeZoneHandling = DateTimeZoneHandling.Utc,
			ContractResolver = new JsonConditionIgnoreContractResolver<JsonIgnoreWhenDB>(),
			Binder = new JsonInterfaceBinder(),
			Converters = new List<JsonConverter>()
			{
				new CustomDateTimeConverter(),
			}
		};

		public static T DeserializeJsonFromPath<T>(string filePath)
		{
			var text = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
			try
			{
				return JsonConvert.DeserializeObject<T>(text, _settings);
			}
			catch (Exception e)
			{
				throw new Exception("Deserialize error in file : " + filePath, e);
			}
		}

		public static string Serialize<T>(T obj)
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
    }
}