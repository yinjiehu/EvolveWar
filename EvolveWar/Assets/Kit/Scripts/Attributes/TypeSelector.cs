using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kit.Inspector
{
	public class TypeSelectorAttribute : PropertyAttribute
	{
		public Type BaseType { set; get; }
		public Type DefaultType { set; get; }
		public string LabelName { set; get; }

		public TypeSelectorAttribute(Type baseType)
		{
			BaseType = baseType;
		}
	}

#if UNITY_EDITOR

	[CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
	public class TypeSelectorPropertyDrawer : PropertyDrawer
	{
		List<string> _tolistTypes;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var attr = (TypeSelectorAttribute)attribute;

			if(_tolistTypes == null)
			{
				_tolistTypes = new List<string>();
				//var types = attr.BaseType.Assembly.GetTypes();

				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach (Type t in assembly.GetTypes())
					{
						if (t.IsInterface)
							continue;

						if (attr.DefaultType != null && t == attr.DefaultType)
						{
							_tolistTypes.Insert(0, t.FullName);
						}
						else if (attr.BaseType.IsInterface && t.GetInterface(attr.BaseType.FullName) != null)
						{
							_tolistTypes.Add(t.FullName);
						}
						else if (t == attr.BaseType || t.IsSubclassOf(attr.BaseType))
						{
							_tolistTypes.Add(t.FullName);
						}
					}
				}
			}

			if(string.IsNullOrEmpty(attr.LabelName))
				EditorGUI.PrefixLabel(position, label);
			else
				EditorGUI.PrefixLabel(position, new GUIContent(attr.LabelName));

			position.x += EditorGUIUtility.labelWidth;
			if (_tolistTypes.Count == 0)
			{
				EditorGUI.LabelField(position, "no sub type for [" + attr.BaseType.FullName + "]");
			}
			else
			{
				var currentValue = property.stringValue;
				var index = _tolistTypes.IndexOf(currentValue);
				if (index < 0) index = 0;
				index = EditorGUI.Popup(position, index, _tolistTypes.ToArray());
				property.stringValue = _tolistTypes[index];
			}
		}
	}
#endif
}