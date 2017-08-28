using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Kit.Performance
{
	public class PeDoCallFunction : PeNode
	{
		public UnityEvent _event;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();
			_event.Invoke();
			PerformanceComplete();
		}
	}
}