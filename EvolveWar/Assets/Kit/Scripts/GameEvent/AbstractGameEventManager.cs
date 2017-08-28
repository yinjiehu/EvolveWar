using UnityEngine;
using System;
using System.Collections.Generic;

namespace Kit.Event
{
	public abstract class AbstractGameEventManager<TEventInfo, TBindingInfo> : MonoBehaviour where TBindingInfo : EventBindingInfo, new()
	{
		protected List<TBindingInfo> _registedInfoList = new List<TBindingInfo>();
        
        protected virtual void RegistEvent(TBindingInfo registInfo)
		{
			_registedInfoList.Add(registInfo);
			_registedInfoList.Sort((e1, e2) => { return e2.Priority - e1.Priority; });
		}

		public virtual void UnregistEvent(TBindingInfo eventBindingInfo)
		{
			_registedInfoList.Remove(eventBindingInfo);
		}
        
        public virtual void TriggerEvent<R>(R eventData) where R : TEventInfo
		{
			var eventList = new List<TBindingInfo>();
			eventList.AddRange(_registedInfoList);

			HashSet<TBindingInfo> toDeleteBindingInfoList = new HashSet<TBindingInfo>();

            using (var itr = eventList.GetEnumerator())
			{
				var control = new EventControl();
				var eventType = eventData.GetType();

				while (itr.MoveNext())
				{
                    var m = itr.Current;
                    if (toDeleteBindingInfoList.Contains(m))
                        continue;

					if (eventType == m.BindingType || eventType.IsSubclassOf(m.BindingType))
					{
						CallEventDelegate(eventData, m, control);

						if (control.RemoveThisBinding)
						{
                            toDeleteBindingInfoList.Add(m);
							control.RemoveThisBinding = false;
						}

						if (control.CancelFollowingFiring)
							break;
					}
				}
			}

			if (toDeleteBindingInfoList.Count != 0)
			{
				_registedInfoList.RemoveAll(info => toDeleteBindingInfoList.Contains(info));
				toDeleteBindingInfoList.Clear();
			}
		}

		protected abstract void CallEventDelegate(TEventInfo eventInfo, TBindingInfo bindingInfo, EventControl control);
	}

	public class EventBindingInfo
	{
		public string ObjectName;
		public Type BindingType;
		public Delegate Callback;
		public int Priority;
	}
	
	public class EventControl
	{
		public bool CancelFollowingFiring { set; get; }
		public bool RemoveThisBinding { set; get; }		
	}
}