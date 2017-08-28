using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeDoTimeElapsed : PeNode
	{
        protected override void PerformanceStart()
        {
            base.PerformanceStart();

            PerformanceComplete();
        }
    }
}