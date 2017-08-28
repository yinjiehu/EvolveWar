using EvolveWar.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundPlayer : MonoBehaviour
{
	[SerializeField]
	AudioSource _effectSound;

	enum PlaySoundType
	{
		Init,
		Call
	}

	[SerializeField]
	PlaySoundType _soundType;

	void OnEnable()
	{
		if(_soundType == PlaySoundType.Init)
		{
			PlaySound();
		}
	}

	public void PlaySound()
	{
		if (Temp.Get().IsPlayingSoundEffect())
		{
			_effectSound.Play();
		}
	}
}
