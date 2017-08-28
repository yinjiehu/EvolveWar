using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Kit.Performance;

namespace  FullMoon.Battle
{
	public class PeUntilTimeElapsed : PeNode
	{
		protected override void PerformancUpdate()
		{
			base.PerformancUpdate();
			PerformanceComplete();
		}
	}
}