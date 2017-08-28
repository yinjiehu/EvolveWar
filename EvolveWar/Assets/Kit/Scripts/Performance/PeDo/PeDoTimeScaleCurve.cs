using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeDoTimeScaleCurve : PeNode
	{
		public float _duration;
		public AnimationCurve _globalSpeedCurve;
		public bool _absoluteCurveTime = false;
		public float _currentScale = 1;

		float _startTimeScale;
		protected override void PerformanceStart()
		{
			base.PerformanceStart();
			_startTimeScale = Time.timeScale;
		}

		protected override void PerformancUpdate()
		{
			base.PerformancUpdate();

			if (_duration == 0)
			{
				Debug.LogError("time scale duration can not be 0!!");
			}
			else
			{
				var x = 0f;
				if (_absoluteCurveTime)
				{
					x = _elapsedTime;
				}
				else
				{
					x = _elapsedTime > _duration ? 1 : _elapsedTime / _duration;
				}
				Time.timeScale = _currentScale = _globalSpeedCurve.Evaluate(x);
			}
			
			if (_elapsedTime > _duration)
			{
				PerformanceComplete();
			}
		}

		public override void ResetToStartStatus()
		{
			base.ResetToStartStatus();
			Time.timeScale = _startTimeScale;
		}
	}
}