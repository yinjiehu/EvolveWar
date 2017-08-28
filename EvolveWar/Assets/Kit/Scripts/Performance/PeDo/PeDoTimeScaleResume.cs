using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeDoTimeScaleResume : PeNode
	{
		public float _resumeTo = 1f;

		protected override void PerformancUpdate()
		{
			base.PerformancUpdate();

			Time.timeScale = _resumeTo;
			PerformanceComplete();
		}
	}
}