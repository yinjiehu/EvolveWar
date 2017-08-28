using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeConcurrentChildren : PeNode
	{
		List<PeNode> _performanceList = new List<PeNode>();
		int _index = 0;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();

			_index = 0;
			_performanceList.Clear();

			var count = transform.childCount;

			for (int i = 0; i< count; i++)
			{
				var p = transform.GetChild(i).GetComponent<PeNode>();
				if (p != null && p.gameObject.activeSelf)
					_performanceList.Add(p);
			}

			foreach (var b in _performanceList)
			{
				b.Play(this, OnSubPerformanceComplete);
			}

			if (_performanceList.Count == 0)
			{
				Debug.LogError("child performance has not been found!!!");
				PerformanceComplete();
			}
		}
		
		protected virtual void OnSubPerformanceComplete()
		{
			_index++;
			if (_index == _performanceList.Count)
			{
				PerformanceComplete();
			
			}
			else if (_index > _performanceList.Count)
			{
				Debug.LogError("complet performance count is greate than list count");
			}
		}

		public override void ResetToStartStatus()
		{
			base.ResetToStartStatus();

			using (var itr = _performanceList.GetEnumerator())
			{
				while (itr.MoveNext())
				{
					itr.Current.ResetToStartStatus();
				}
			}
		}
		public override void ChangeTimeScale(float s)
		{
			base.ChangeTimeScale(s);

			using (var itr = _performanceList.GetEnumerator())
			{
				while (itr.MoveNext())
				{
					itr.Current.ChangeTimeScale(s);
				}
			}
		}
    }
}