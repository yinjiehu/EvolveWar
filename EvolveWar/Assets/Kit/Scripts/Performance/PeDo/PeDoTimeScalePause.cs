using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeDoTimeScalePause : PeNode
	{
		protected override void PerformancUpdate()
		{
			base.PerformancUpdate();

			Time.timeScale = 0;
			PerformanceComplete();
		}
	}
}