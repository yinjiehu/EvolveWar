using UnityEngine;
using System;

namespace Kit.Event
{
	public class GlobalEventManager : AbstractGameEventManager<GlobalEventInfo, EventBindingInfo>
	{
		static GlobalEventManager _instance;
		public static GlobalEventManager Instance
		{
			get
			{
				if (_instance == null)
				{
					var go = new GameObject("GlobalEventManager");
					_instance = go.AddComponent<GlobalEventManager>();
					DontDestroyOnLoad(go);
				}

				return _instance;
			}
		}

		public void OnDestroy()
		{
			_instance = null;
		}

		public virtual EventBindingInfo RegistEvent(Type registType, Action<GlobalEventInfo, EventControl> callback, int priority = 0)
		{
			EventBindingInfo info = new EventBindingInfo();
			info.BindingType = registType;
			info.Callback = callback;
			info.Priority = priority;

			base.RegistEvent(info);
			return info;
		}


		public EventBindingInfo RegistEvent<R>(Action<R, EventControl> callback, int priority = 0) where R : GlobalEventInfo
		{
			EventBindingInfo info = new EventBindingInfo();
			info.BindingType = typeof(R);
			info.Callback = callback;
			info.Priority = priority;

			base.RegistEvent(info);
			return info;
		}
		
		protected override void CallEventDelegate(GlobalEventInfo eventInfo, EventBindingInfo bindingInfo, EventControl control)
		{
			bindingInfo.Callback.DynamicInvoke(eventInfo, control);
		}
	}

	public class GlobalEventInfo
	{

	}

	public class LoadNextSceneStartEvent : GlobalEventInfo
	{
		public bool IsNextLoadingScene { set; get; }
		public string NextSceneName { set; get; }
	}

	public class SceneAwakeEventInfo : GlobalEventInfo
	{
		public bool IsLoadingScene { set; get; }
		public string SceneName { set; get; }
	}
}