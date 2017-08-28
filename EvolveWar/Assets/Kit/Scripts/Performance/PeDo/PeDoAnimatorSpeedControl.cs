using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeDoAnimatorSpeedControl : PeNode
	{
		[SerializeField]
		AnimatorTarget _targetAnimator;

		[SerializeField]
		float _duration;

		[SerializeField]
		AnimationCurve _curve;

		[SerializeField]
		bool _absoluteCurveTime = false;

		[SerializeField]
		float _currentAnimatorSpeed = 1;

		float startSpeed;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();
			startSpeed = _targetAnimator.GetTarget(transform).speed;
		}

		protected override void PerformancUpdate()
		{
			base.PerformancUpdate();

			var x = _absoluteCurveTime ? _elapsedTime : _elapsedTime / _duration;

			_targetAnimator.GetTarget(transform).speed = _currentAnimatorSpeed = _curve.Evaluate(x);
			
			if (_elapsedTime > _duration)
			{
				PerformanceComplete();
			}
		}

		public override void ResetToStartStatus()
		{
			base.ResetToStartStatus();
			_targetAnimator.GetTarget(transform).speed = startSpeed;
		}
	}
}