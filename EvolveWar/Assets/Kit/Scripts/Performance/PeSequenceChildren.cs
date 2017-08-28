using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeSequenceChildren : PeNode
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
				{
					_performanceList.Add(p);
					p.name = i + "_" + p.name;
				}
			}
			
			DoNext();
		}

		protected virtual void DoNext()
		{
			if (_index < _performanceList.Count)
			{
				var next = _performanceList[_index];
				_index++;
				
				//Log.i("seq next " + next.name);
				next.Play(this, DoNext);
			}
			else
			{
				PerformanceComplete();
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