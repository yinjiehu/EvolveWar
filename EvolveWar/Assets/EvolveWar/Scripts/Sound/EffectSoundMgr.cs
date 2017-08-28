using EvolveWar.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundMgr : MonoBehaviour
{
	static EffectSoundMgr _instance;

	public static EffectSoundMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject go = new GameObject("EffectSoundMgr");
				_instance = go.AddComponent<EffectSoundMgr>();
			}
			return _instance;
		}
	}

	[SerializeField]
	AudioSource _expSound;
	[SerializeField]
	AudioSource _bloodSound;
	[SerializeField]
	AudioSource _boxSound;
	[SerializeField]
	AudioSource _addSkillSound;
	[SerializeField]
	AudioSource _deleteSkillSound;
	[SerializeField]
	AudioSource _addInvalidSkillSound;
	[SerializeField]
	AudioSource _skillLevelUpSound;
	[SerializeField]
	AudioSource _battleLevelUpSound;
	[SerializeField]
	AudioSource _attackSound;
	[SerializeField]
	AudioSource _killSound;
	[SerializeField]
	AudioSource _deadSound;
	[SerializeField]
	AudioSource _clickSound;
	[SerializeField]
	AudioSource _openPanelSound;
	[SerializeField]
	AudioSource _closePanelSound;
	[SerializeField]
	AudioSource _playerLevelUpSound;
	[SerializeField]
	AudioSource _talentLevelUpSound;

	AudioSource _currentSound;

	void Awake()
	{
		_instance = this;
	}

	void PlaySound(AudioSource source)
	{
		if (Temp.Get().IsPlayingSoundEffect())
		{
			source.Play();
		}
	}

	public void PlayExpSound()
	{
		PlaySound(_expSound);
	}

	public void PlayBloodSound()
	{
		PlaySound(_bloodSound);
	}

	public void PlayBoxSound()
	{
		PlaySound(_boxSound);
	}

	public void PlayAddSkillSound()
	{
		PlaySound(_addSkillSound);
	}

	public void PlayDeleteSkillSound()
	{
		PlaySound(_deleteSkillSound);
	}

	public void PlayAddInvalidSound()
	{
		PlaySound(_addInvalidSkillSound);
	}

	public void PlaySkillLevelUpSound()
	{
		PlaySound(_skillLevelUpSound);
	}

	public void PlayBattleLevelUpSound()
	{
		PlaySound(_battleLevelUpSound);
	}

	public void PlayAttackSound()
	{
		PlaySound(_attackSound);
	}

	public void PlayKillSound()
	{
		PlaySound(_killSound);
	}

	public void PlayDeadSound()
	{
		PlaySound(_deadSound);
	}

	public void PlayClickSound()
	{
		PlaySound(_clickSound);
	}

	public void PlayOpenPanelSound()
	{
		PlaySound(_openPanelSound);
	}

	public void PlayClosePanelSound()
	{
		PlaySound(_closePanelSound);
	}

	public void PlayPlayerLevelUpSound()
	{
		PlaySound(_playerLevelUpSound);
	}

	public void PlayTalentLevelUpSound()
	{
		PlaySound(_talentLevelUpSound);
	}
}
