using UnityEngine;
using System.Collections.Generic;

namespace Kit.Performance
{
	public class PeDoAudioLinearFadeOut : PeNode
	{
		public AudioSource _target;
		public float _duration;
		public bool _fadeFromCurrent = true;
		public float _startVolume;
		public float _targetVolume;

		protected override void PerformanceStart()
		{
			base.PerformanceStart();
			if (_fadeFromCurrent)
				_startVolume = _target.volume;
		}

		protected override void PerformancUpdate()
		{
			base.PerformancUpdate();

			if (_duration == 0)
			{
				Debug.LogError("time scale duration can not be 0!!");
			}
			else if (_elapsedTime > _duration)
			{
				_target.volume = _targetVolume;
				PerformanceComplete();
			}
			else
			{
				_target.volume = _startVolume + (_targetVolume - _startVolume) * _elapsedTime / _duration;
			}
		}

		public override void ResetToStartStatus()
		{
			base.ResetToStartStatus();
			_target.volume = _startVolume;
		}


		public static void SimpleFadeOut(AudioSource audioSource, float duration, float delay, Component caller, System.Action completeCallback)
		{
			var go = new GameObject();
			go.name = "PeAudioLinearFadeOut : " + audioSource.name;
			var fadeout = go.AddComponent<PeDoAudioLinearFadeOut>();
			fadeout._target = audioSource;
			fadeout._duration = duration;
			fadeout._delay = delay;

			fadeout._destroyWhenComplete = true;
			fadeout.Play(caller, completeCallback);
		}
	}
}