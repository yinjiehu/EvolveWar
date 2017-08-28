#if UNITY_EDITOR
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Kit.Utility
{
	public class UtilitySerializeHelper : MonoBehaviour
	{

		////[FullInspector.InspectorButton]
		public void SerializeAllToJsonFile()
		{
			var directory = GetOutPutFoloder();
			if (!string.IsNullOrEmpty(directory) && Directory.Exists(directory))
			{
				var optin = EditorUtility.DisplayDialogComplex("Write Data File", "将要输出文件 \n" + directory, "确定", "取消", "选择路径");
				if (optin == 0)
				{
					var monos = GetComponentsInChildren<MonoBehaviour>();
					foreach(var m in monos)
					{
						if(m is ISerializeToJson)
						{
							((ISerializeToJson)m).SerializeToJson(false);
							//OutPutFile(m, directory);
						}
					}
				}
				else if (optin == 2)
				{
					SetDataFileWriteDirectory();
					SerializeAllToJsonFile();
				}
			}
		}

		public static void WriteToFile(object obj, string fileName, bool dialogConfirm = true)
		{
			var directory = GetOutPutFoloder();
			if (!string.IsNullOrEmpty(directory) && Directory.Exists(directory))
			{
				if (string.IsNullOrEmpty(fileName))
					fileName = obj.GetType().Name;
				var outputFullPath = directory + "/" + fileName + ".json";

				if (!dialogConfirm)
				{
					OutPutFile(obj, outputFullPath);
					return;
				}

				var optin = EditorUtility.DisplayDialogComplex("Write Data File", "将要输出文件 \n" + outputFullPath, "确定", "取消", "选择路径");
				if (optin == 0)
					OutPutFile(obj, outputFullPath);
				else if (optin == 2)
				{
					SetDataFileWriteDirectory();
					WriteToFile(obj, fileName);
				}
			}
		}

		public static string GetOutPutFoloder()
		{
			var directory = EditorPrefs.GetString("DataOutPutDirectory");

			if (string.IsNullOrEmpty(directory))
			{
				if (EditorUtility.DisplayDialog("Write File Error!", "没有设定输出目标文件夹", "设置", "取消"))
				{
					SetDataFileWriteDirectory();
					return GetOutPutFoloder();
				}

				return "";
			}
			else if (!Directory.Exists(directory))
			{
				if (EditorUtility.DisplayDialog("Write File Error!", "输出目标文件夹设定有误，请重新设置", "设置", "取消"))
				{
					SetDataFileWriteDirectory();
					return GetOutPutFoloder();
				}

				return "";
			}
			return directory;
		}

		[MenuItem("Data/SetOutPutFolder")]
		public static void SetDataFileWriteDirectory()
		{
			var directory = EditorPrefs.GetString("DataOutPutDirectory");
			if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
				directory = Application.dataPath + "/../../../Server/IronFuryServer/IronFury/Resources/";

			directory = EditorUtility.SaveFolderPanel("选择数据文件输出文件夹", directory, "");

			if(!string.IsNullOrEmpty(directory))
				EditorPrefs.SetString("DataOutPutDirectory", directory);
		}

		static void OutPutFile(object obj, string outputFullPath)
		{
			var settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto,
				TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
				NullValueHandling = NullValueHandling.Include,
				ContractResolver = new IgnoreUnityObjectContractResolver()
			};

			var content = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);

			try
			{
				File.WriteAllText(outputFullPath, content, Encoding.UTF8);
				Debug.Log("Write File Complete. " + outputFullPath + " \n" + content);
			}
			catch (Exception e)
			{
				EditorUtility.DisplayDialog("Write File Error!", "输出文件错误 \n" + e.Message, "确定");
				Debug.LogException(e);
			}
		}
	}
}
#endif