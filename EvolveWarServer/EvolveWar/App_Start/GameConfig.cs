using System.Linq;
using System;
using System.Reflection;
using System.Collections.Generic;
using Kit.Json;

namespace EvolveWar
{
	public class GameConfig
	{

		static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

		public static void Initialize()
		{
			_logger.Debug("Config game initialize");
			LoadJson();
		}

		static void LoadJson()
		{
			var properties = typeof(Config).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (var property in properties)
			{
				var loadFromJsonAttr = property.GetCustomAttribute<LoadFromJsonFileAttribute>();
				if (loadFromJsonAttr != null)
				{
					var settingsName = string.IsNullOrEmpty(loadFromJsonAttr.CustomName) ? property.Name : loadFromJsonAttr.CustomName;
					LoadFromJsonFile(property, settingsName);
				}
			}
		}


		static void LoadFromJsonFile(PropertyInfo property, string settingsName)
		{
			var base_path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/Resources/";
			var fullPath = base_path + settingsName + ".json";

			_logger.Debug("Load global config from json file [{0}] ", fullPath);

			var text = System.IO.File.ReadAllText(fullPath, System.Text.Encoding.UTF8);

			_logger.Debug("Load global config from json text [{0}] ", text);
			//_logger.Debug("json file content [{0}] ", text);
			object deserializedObj = JsonUtilDB.Deserialize(text, property.PropertyType);
			property.SetValue(null, deserializedObj);

			_logger.Debug("Load global config from json data [{0}] ", deserializedObj);
		}
	}
}