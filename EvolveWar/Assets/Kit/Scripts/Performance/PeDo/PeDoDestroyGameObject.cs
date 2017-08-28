using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Kit.Performance
{
	public class PeDoDestroyGameObject : PeNode
	{
		public TransformTarget _target;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();
			Destroy(_target.GetTarget(transform).gameObject);
			PerformanceComplete();
		}
	}
}