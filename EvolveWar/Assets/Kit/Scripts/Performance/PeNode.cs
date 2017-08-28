using UnityEngine;
using System;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kit.Performance
{
	public class PeNode : MonoBehaviour
	{
		[SerializeField]
		UnityEvent _onStartEvent;
		[SerializeField]
		UnityEvent _onCompleteEvent;
		[SerializeField]
		UnityEvent _onUpdateEvent;

		string _peName;
		string normalName;
		public bool _ignoreTimeScale;
		public float _delay;
		public float _timeScale = 1;

		public enum SeqTypeEnum
		{
			PerformanceComplete,
			PerformanceStart,
			Immidiate
		}
		public SeqTypeEnum SeqeuceType = SeqTypeEnum.PerformanceComplete;

		public enum PeStatus
		{
			NotActive,
			Delay,
			Active,
			Cancel
		}
		protected PeStatus _status = PeStatus.NotActive;
		public PeStatus Status { get { return _status; } }

		protected float _elapsedTime = 0;
		protected float _deltaTime = 0;

		protected bool _destroyWhenComplete;
		protected Component _caller;
		protected Action _completeCallback;
		protected float _orignTimeScale;

		protected virtual void Awake()
		{
			_orignTimeScale = _timeScale;
			normalName = name;
		}

		public void Play()
		{
			Play(null, null);
		}

		public virtual PeNode PlayCopy(Component caller, Action callback, bool destroyCopyWhenComplete = true)
		{
			var p = Instantiate(this);
			p.transform.SetParent(this.transform.parent);
			p.transform.position = this.transform.position;
			p.transform.rotation = this.transform.rotation;
			p.transform.localScale = this.transform.localScale;

			p.Play(this, delegate
				 {
					 if (caller != null && callback != null && Status != PeStatus.Cancel)
					 {
						 callback();
					 }

					 if (destroyCopyWhenComplete)
						 Destroy(p.gameObject);
				 });
			return p;
		}

		public virtual void Play(Component caller, Action onCompleteCallback)
		{

			if (!gameObject.activeInHierarchy)
			{
				onCompleteCallback();
				return;
			}

			_elapsedTime = 0;
			_caller = caller;
			_completeCallback = onCompleteCallback;

			_peName = name;
			name = "(delay)" + _peName;

			if (_delay <= 0)
			{
				_status = PeStatus.Active;
				PerformanceStart();
			}
			else
			{
				_status = PeStatus.Delay;
			}
			if (SeqeuceType == SeqTypeEnum.Immidiate)
			{
				if (_caller != null && _completeCallback != null)
				{
					_completeCallback();
				}
			}
		}

		protected virtual void PerformanceStart()
		{
			name = "(exe)" + _peName;
			_onStartEvent.Invoke();
		}

		void Update()
		{
			if (_ignoreTimeScale)
				_deltaTime = Time.unscaledDeltaTime * _timeScale;
			else
				_deltaTime = Time.deltaTime * _timeScale;

			OnUpdate();
		}

		void OnUpdate()
		{
			if (_status == PeStatus.Delay)
			{
				_elapsedTime += _deltaTime;
				if (_elapsedTime > _delay)
				{
					_elapsedTime -= _delay;
					_status = PeStatus.Active;

					if (SeqeuceType == SeqTypeEnum.PerformanceStart)
					{
						if (_caller != null && _completeCallback != null)
						{
							_completeCallback();
						}
					}
					PerformanceStart();
				}
			}
			else if (_status == PeStatus.Active)
			{
				_elapsedTime += _deltaTime;
				PerformancUpdate();
			}
		}

		protected virtual void PerformancUpdate()
		{
			_onUpdateEvent.Invoke();
		}

		public virtual void PerformanceComplete()
		{
			if (_status != PeStatus.NotActive)
			{
				_status = PeStatus.NotActive;
				name = normalName;
				if (SeqeuceType == SeqTypeEnum.PerformanceComplete)
				{
					_onCompleteEvent.Invoke();
					if (_caller != null && _completeCallback != null)
					{
						_completeCallback();
					}
				}
				if (_destroyWhenComplete)
				{
					Destroy(gameObject);
				}
			}
			else
			{
				Debug.LogError("Performance complete [" + name + "] is not called in running mode");
			}
		}

		public virtual void ResetToStartStatus()
		{
		}

		public virtual void ChangeTimeScale(float s)
		{
			_timeScale = s;
		}
	}


#if UNITY_EDITOR
	[CustomEditor(typeof(PeNode), true)]
	public class PeNodEditor : Editor
	{
		int _selectedPageIndex;
		public override void OnInspectorGUI()
		{
			EditorGUILayout.Space();
			_selectedPageIndex = GUILayout.Toolbar(_selectedPageIndex, new string[] { "Settings", "Events" }, EditorStyles.miniButton);
			EditorGUILayout.Space();
			Rect rect = EditorGUILayout.GetControlRect(false, 1f);
			EditorGUI.DrawRect(rect, EditorStyles.label.normal.textColor * 0.5f);
			serializedObject.Update();

			switch (_selectedPageIndex)
			{
				case 0:
					//EditorGUILayout.PropertyField(serializedObject.FindProperty("_ignoreTimeScale"));
					//EditorGUILayout.PropertyField(serializedObject.FindProperty("_delay"));
					var property = serializedObject.FindProperty("_ignoreTimeScale");
					EditorGUILayout.PropertyField(property);
					while (property.NextVisible(false))
					{
						EditorGUILayout.PropertyField(property, true);
					}

					EditorGUILayout.Space();
					break;
				case 1:
					EditorGUILayout.PropertyField(serializedObject.FindProperty("_onStartEvent"));
					EditorGUILayout.Space();
					EditorGUILayout.PropertyField(serializedObject.FindProperty("_onCompleteEvent"));
					EditorGUILayout.Space();
					EditorGUILayout.PropertyField(serializedObject.FindProperty("_onUpdateEvent"));
					EditorGUILayout.Space();
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}