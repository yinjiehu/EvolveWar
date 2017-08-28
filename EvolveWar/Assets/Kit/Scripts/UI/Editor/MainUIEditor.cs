using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Kit.UI
{

	[CustomEditor(typeof(MainUI))]
	public class MainUIEditor : Editor
	{
		SerializedProperty
			_paths;

		SerializedObject _serializeObject;

		void OnEnable()
		{
			_serializeObject = new SerializedObject(target);

		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			DrawDefaultInspector();
			_paths = _serializeObject.FindProperty("_paths");
			if (GUILayout.Button("Init UI List"))
			{
				LoadUIList();
			}
		}

		void LoadUIList()
		{
			((MainUI)target).InitPanel();
			_paths = _serializeObject.FindProperty("_paths");
			foreach (SerializedProperty p in _paths)
			{
				if (p != null)
				{
					Debug.Log("path:" + p.stringValue);
					string path = Application.dataPath + "" + p.stringValue;

					var dir = Directory.GetFiles(path);
					if (Directory.Exists(path))
					{
						foreach (var filePath in dir)
						{
							int index = filePath.IndexOf("Assets");
							string newpath = filePath.Substring(index, filePath.Length - index);
							var obj = AssetDatabase.LoadAssetAtPath<BasePanel>(newpath);
							if (obj != null)
							{
								((MainUI)target).AddPanel(obj as BasePanel);
							}
						}
					}
					else
					{
						Debug.Log("No directory " + path);
					}
				}

			}

			EditorUtility.DisplayDialog("", "完成", "OK");
		}
	}


}