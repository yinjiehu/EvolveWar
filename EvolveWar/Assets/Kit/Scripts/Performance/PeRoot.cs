using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeRoot : PeConcurrentChildren
    {
        public bool _applyTimeScaleToChildren;
		public bool _autoStart;

		void Start()
		{
			if (_autoStart)
				Play();

			//var scheduler = Scheduler.Instance;
			//if (scheduler == null)
			//	Debug.LogError("scheduler has not been added to the scene!!!!!!!!!!!!!!");

			var pes = GetComponentsInChildren<PeNode>();
			foreach (var pe in pes)
			{
                if (pe == this)
                    continue;

				pe._ignoreTimeScale = _ignoreTimeScale;

                if(_applyTimeScaleToChildren)
                    pe._timeScale = _timeScale;
			}
		}

        public override void ChangeTimeScale(float s)
        {
            _timeScale = s;
            var pes = GetComponentsInChildren<PeNode>();
            foreach (var pe in pes)
            {
                pe._timeScale = _timeScale;
            }
        }

        public void QuicklyReach()
        {
            var pes = GetComponentsInChildren<PeNode>();
            foreach (var pe in pes)
            {
                if(pe.Status != PeStatus.NotActive)
                {
                    pe._timeScale = int.MaxValue;
                }
                
            }
        }

    }
}