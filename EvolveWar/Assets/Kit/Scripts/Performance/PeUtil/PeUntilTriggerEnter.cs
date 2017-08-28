using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Kit.Performance;

namespace  FullMoon.Battle
{
	public class PeUntilTriggerEnter : PeNode
	{
		[SerializeField]
		ComponentTarget _target;
		
		bool _isRunning;
		bool _complete;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();
			_isRunning = true;
		}

		protected override void PerformancUpdate()
		{
			base.PerformancUpdate();
			if(_complete)
			{
				PerformanceComplete();
			}
		}

		public void OnTriggerEnter(Collider other)
		{
			if (_isRunning)
			{
				if(other.transform == _target.GetTarget(transform).transform)
					_complete = true;
			}
		}

		public override void ResetToStartStatus()
		{
			base.ResetToStartStatus();
			_isRunning = false;
			_complete = false;
		}
	}
}