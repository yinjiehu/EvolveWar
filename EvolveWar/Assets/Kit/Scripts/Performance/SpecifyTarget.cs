using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kit.Performance
{
	public abstract class TargetSpecifyExtend : MonoBehaviour
	{
		public abstract GameObject GetTarget();
	}

	public enum SpecifyTypeEnum
	{
		DirectRefenrence,
		FindObjectOfType,
		GetComponentInParent,
		GetComponentInChildren,
		Find,
		FindChild,
		Extend = 100
	}

	[Serializable]
	public abstract class SpecifyTarget { }

	public class SpecifyTarget<T> : SpecifyTarget where T : Component
	{

		[SerializeField]
		protected SpecifyTypeEnum _specifyBy;

		//[SerializeField]
		//protected string _targetType;

		[SerializeField]
		protected T _target;

		[SerializeField, Range(0, 10)]
		protected int _parentLevel;

		[SerializeField]
		protected string _findString;

		[SerializeField]
		protected string _targetTypeString;

		protected T _runtimeTarget;
				
		protected virtual bool ShowOnlyDirectReference()
		{
			return _specifyBy == SpecifyTypeEnum.DirectRefenrence;
		}
		protected virtual bool ShowParentLevel()
		{
			return _specifyBy == SpecifyTypeEnum.GetComponentInChildren || _specifyBy == SpecifyTypeEnum.FindChild;
		}
		protected virtual bool ShowFindAndFindChild()
		{
			return _specifyBy == SpecifyTypeEnum.Find || _specifyBy == SpecifyTypeEnum.FindChild;
		}

		public T GetTarget(Transform caller)
		{
			if(_runtimeTarget == null)
			{
				Type type = typeof(T);
				
				switch (_specifyBy)
				{
					case SpecifyTypeEnum.DirectRefenrence:
						_runtimeTarget = _target;
						break;
					case SpecifyTypeEnum.FindObjectOfType:
						_runtimeTarget = UnityEngine.Object.FindObjectOfType(type) as T;
						break;
					case SpecifyTypeEnum.GetComponentInParent:
						_runtimeTarget = caller.GetComponentInParent(type) as T;
						break;
					case SpecifyTypeEnum.GetComponentInChildren:
						_runtimeTarget = GetParentByLevel(caller, _parentLevel).GetComponentInChildren(type) as T;
						break;
					case SpecifyTypeEnum.Find:
						{
							var t = GameObject.Find(_findString);
							if (t == null)
							{
								Debug.LogError("Can not find game object named " + _findString, caller);
								return null;
							}
							_runtimeTarget = GameObject.Find(_findString).GetComponent(type) as T;
						}
						break;
					case SpecifyTypeEnum.FindChild:
						{
							var t = GetParentByLevel(caller, _parentLevel);
							t = t.Find(_findString);
							if (t == null)
							{
								Debug.LogError("Can not find child " + _findString, caller);
								return null;
							}
							_runtimeTarget = t.GetComponent(type) as T;
						}
						break;
					case SpecifyTypeEnum.Extend:
						var coms = caller.GetComponents<Component>();
						TargetSpecifyExtend _specifyTargetExtend = null;
						for (int i = 0; i < coms.Length; i++)
						{
							if(coms[i] is TargetSpecifyExtend)
							{
								_specifyTargetExtend = (TargetSpecifyExtend)coms[i];
								break;
							}
						}
						if(_specifyTargetExtend == null)
						{
							Debug.LogError("Specify type is set to extend but ISpecifyTargetExtend is not found.", caller);
							return null;
						}
						_runtimeTarget = _specifyTargetExtend.GetTarget().GetComponent(type) as T;
						break;
				}
			}
			return _runtimeTarget;
		}
		Transform GetParentByLevel(Transform t, int parentLevel)
		{
			var p = t;
			for (int i = 0; i < parentLevel && p.parent != null; i++)
			{
				p = p.parent;
			}
			return p;
		}
	}


#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(SpecifyTarget), true)]
	public class SpecifyTargetDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!property.isExpanded)
				return EditorGUIUtility.singleLineHeight;

			var line = 2;

			var specifyProperty = property.FindPropertyRelative("_specifyBy");
			var specifyBy = (SpecifyTypeEnum)specifyProperty.intValue;
			switch (specifyBy)
			{
				case SpecifyTypeEnum.DirectRefenrence:
					line += 1;
					break;
				case SpecifyTypeEnum.FindObjectOfType:
					line += 1;
					break;
				case SpecifyTypeEnum.GetComponentInParent:
					line += 2;
					break;
				case SpecifyTypeEnum.GetComponentInChildren:
					line += 1;
					break;
				case SpecifyTypeEnum.Find:
					line += 2;
					break;
				case SpecifyTypeEnum.FindChild:
					line += 3;
					break;
				case SpecifyTypeEnum.Extend:
					line += 2;
					break;
			}

			return EditorGUIUtility.singleLineHeight * line;
		}

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			UnityEngine.Object target = property.serializedObject.targetObject;

			rect.height = EditorGUIUtility.singleLineHeight;
			if (EditorGUI.PropertyField(rect, property))
			{
				rect.x += 10;
				rect.width -= 10;
				rect.y += rect.height;

				EditorGUI.BeginChangeCheck();
				var specifyProperty = property.FindPropertyRelative("_specifyBy");
				var specifyBy = (SpecifyTypeEnum)EditorGUI.EnumPopup(rect, "Specify Target", (SpecifyTypeEnum)specifyProperty.intValue);
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(target, "Specify Target");
					specifyProperty.intValue = (int)specifyBy;
					EditorUtility.SetDirty(target);
				}
				rect.y += rect.height;

				if (specifyBy == SpecifyTypeEnum.DirectRefenrence)
				{
					var directReference = property.FindPropertyRelative("_target");
					EditorGUI.ObjectField(rect, directReference, new GUIContent("Target"));
					rect.y += rect.height;
				}
				else if (specifyBy == SpecifyTypeEnum.FindObjectOfType)
				{
					rect = DrawTypeSelector(rect, property);
				}
				else if (specifyBy == SpecifyTypeEnum.GetComponentInParent)
				{
					rect = DrawTypeSelector(rect, property);

					EditorGUI.PropertyField(rect, property.FindPropertyRelative("_parentLevel"), new GUIContent("Parent Level"));
					rect.y += rect.height;
				}
				else if (specifyBy == SpecifyTypeEnum.GetComponentInChildren)
				{
					rect = DrawTypeSelector(rect, property);
				}
				else if (specifyBy == SpecifyTypeEnum.Find)
				{
					rect = DrawTypeSelector(rect, property);

					EditorGUI.PropertyField(rect, property.FindPropertyRelative("_findString"), new GUIContent("Find String"));
					rect.y += rect.height;
				}
				else if (specifyBy == SpecifyTypeEnum.FindChild)
				{
					rect = DrawTypeSelector(rect, property);

					EditorGUI.PropertyField(rect, property.FindPropertyRelative("_parentLevel"), new GUIContent("Parent Level"));
					rect.y += rect.height;
					EditorGUI.PropertyField(rect, property.FindPropertyRelative("_findString"), new GUIContent("Find String"));
					rect.y += rect.height;
				}
				else if (specifyBy == SpecifyTypeEnum.Extend)
				{
					rect = DrawTypeSelector(rect, property);

					EditorGUI.PropertyField(rect, property.FindPropertyRelative("_specifyTargetExtend"), new GUIContent("Extend"));
					rect.y += rect.height;
				}
			}
		}


		Rect DrawTypeSelector(Rect rect, SerializedProperty property)
		{
			var propertyType = fieldInfo.FieldType;
			while (propertyType.GetGenericArguments().Length == 0 || propertyType.GetGenericTypeDefinition() != typeof(SpecifyTarget<>))
			{
				propertyType = propertyType.BaseType;
			}
			var baseType = propertyType.GetGenericArguments()[0];

			var allTypes = new List<string>();
			allTypes.Add(baseType.FullName.Replace('.', '/'));
			allTypes.AddRange(GetType().Assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)).Select(t => t.FullName.Replace('.', '/')));
			allTypes.AddRange(baseType.Assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)).Select(t => t.FullName.Replace('.', '/')));
			//if(genericType.Assembly == this.GetType().Assembly)

			var typeSelectString = property.FindPropertyRelative("_targetTypeString");
			//if (allTypes.Count == 0)
			//{
			//	typeSelectString.stringValue = "";
			//	EditorGUI.LabelField(rect, "Target", genericType.FullName);
			//}
			//else
			{
				var value = typeSelectString.stringValue.Replace('.', '/');
				var index = allTypes.IndexOf(value);
				index = index < 0 ? 0 : index;

				index = EditorGUI.Popup(rect, property.displayName, index, allTypes.ToArray());
				typeSelectString.stringValue = allTypes[index].Replace("/", ".");
			}

			rect.y += rect.height;
			return rect;
		}
	}
#endif

	[Serializable]
	public class ComponentTarget : SpecifyTarget<Component> { }
	[Serializable]
	public class TransformTarget : SpecifyTarget<Transform> { }
	[Serializable]
	public class RectTransformTarget : SpecifyTarget<RectTransform> { }
	[Serializable]
	public class AnimatorTarget : SpecifyTarget<Animator> { }
	[Serializable]
    public class TextTarget : SpecifyTarget<Text> { }


	//[Serializable]
	//public class SpecifyGameObject
	//{
	//	Type _rootType;
	//	public Type RootType { set { _rootType = value; } }

	//	public enum SpecifyGameObjectTypeEnum
	//	{
	//		DirectRefenrence,
	//		Find,
	//		FindChild,
	//		Extend = 100
	//	}
	//	[SerializeField]
	//	protected SpecifyGameObjectTypeEnum _specifyBy;
		
	//	[SerializeField, InspectorShowIf("ShowOnlyDirectReference")]
	//	protected GameObject _target;

	//	[SerializeField, InspectorShowIf("ShowParentLevel"), Range(0, 10)]
	//	protected int _parentLevel;

	//	[SerializeField, InspectorShowIf("ShowFindAndFindChild")]
	//	protected string _findString;

	//	protected GameObject _runtimeTarget;

	//	protected virtual bool ShowOnlyDirectReference()
	//	{
	//		return _specifyBy == SpecifyGameObjectTypeEnum.DirectRefenrence;
	//	}
	//	protected virtual bool ShowParentLevel()
	//	{
	//		return _specifyBy == SpecifyGameObjectTypeEnum.FindChild;
	//	}
	//	protected virtual bool ShowFindAndFindChild()
	//	{
	//		return _specifyBy == SpecifyGameObjectTypeEnum.Find || _specifyBy == SpecifyGameObjectTypeEnum.FindChild;
	//	}

	//	public GameObject GetTarget(Transform caller)
	//	{
	//		if (_runtimeTarget == null)
	//		{
	//			switch (_specifyBy)
	//			{
	//				case SpecifyGameObjectTypeEnum.DirectRefenrence:
	//					_runtimeTarget = _target;
	//					break;
	//				case SpecifyGameObjectTypeEnum.Find:
	//					{
	//						var t = GameObject.Find(_findString);
	//						if (t == null)
	//						{
	//							Debug.LogError("Can not find game object named " + _findString, caller);
	//							return null;
	//						}
	//						_runtimeTarget = t;
	//					}
	//					break;
	//				case SpecifyGameObjectTypeEnum.FindChild:
	//					{
	//						var t = GetParentByLevel(caller, _parentLevel);
	//						t = t.FindChild(_findString);
	//						if (t == null)
	//						{
	//							Debug.LogError("Can not find child " + _findString, caller);
	//							return null;
	//						}
	//						_runtimeTarget = t.gameObject;
	//					}
	//					break;
	//				case SpecifyGameObjectTypeEnum.Extend:
	//					var specifyTargetExtend = caller.GetComponent<TargetSpecifyExtend>();
	//					if (specifyTargetExtend == null)
	//					{
	//						Debug.LogError("Specify type is set to extend but ISpecifyTargetExtend is not found.", caller);
	//						return null;
	//					}
	//					_runtimeTarget = specifyTargetExtend.GetTarget();
	//					break;
	//			}
	//		}
	//		return _runtimeTarget;
	//	}
	//	Transform GetParentByLevel(Transform t, int parentLevel)
	//	{
	//		var p = t;
	//		for (int i = 0; i < parentLevel && p.parent != null; i++)
	//		{
	//			p = p.parent;
	//		}
	//		return p;
	//	}
	//}
}