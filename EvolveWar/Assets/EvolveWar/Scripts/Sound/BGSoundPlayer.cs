using EvolveWar.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundPlayer : MonoBehaviour
{

	static BGSoundPlayer _instance;

	public static BGSoundPlayer Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject go = new GameObject("BGSoundPlayer");
				_instance = go.AddComponent<BGSoundPlayer>();
			}
			return _instance;
		}
	}

	[SerializeField]
	AudioSource _loginBgSound;
	[SerializeField]
	AudioSource _mainBgSound;
	[SerializeField]
	AudioSource _battleBgSound;

	AudioSource _currentSound;

	void Awake()
	{
		_instance = this;
	}

	void CloseCurrentSound()
	{
		if (_currentSound != null)
		{
			_currentSound.Stop();
		}
	}

	void PlaySound(AudioSource source)
	{
		if(Temp.Get().IsPlayingSoundBG())
		{
			source.Play();
		}
		_currentSound = source;
	}

	public void PlayLoginSound()
	{
		CloseCurrentSound();
		PlaySound(_loginBgSound);
	}

	public void PlayMainSound()
	{
		CloseCurrentSound();
		PlaySound(_mainBgSound);
	}

	public void PlayBattleSound()
	{
		CloseCurrentSound();
		PlaySound(_battleBgSound);
	}

	public void CloseSound()
	{
		if(_currentSound != null)
		{
			_currentSound.Stop();
		}
	}

	public void OpenSound()
	{
		if (_currentSound != null)
		{
			_currentSound.Play();
		}
	}
}
