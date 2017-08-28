using EvolveWar.Data;
using Kit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI.Main
{
    public class SettingsPanel : BasePanel
    {
		[SerializeField]
		ToggleStatus _soundBg;
		[SerializeField]
		ToggleStatus _soundEffect;

		public override void ShowPanel(object obj)
        {
            base.ShowPanel(obj);
			EffectSoundMgr.Instance.PlayOpenPanelSound();
			_soundBg.ShowStatus(PlayerPrefs.GetInt("SoundBG", 0) == 0);
			_soundEffect.ShowStatus(PlayerPrefs.GetInt("SoundEffect", 0) == 0);
		}

		public override void HidePanel()
		{
			base.HidePanel();

			EffectSoundMgr.Instance.PlayOpenPanelSound();
		}

		public void OnSoundBgClick(bool isOn)
		{
			if (isOn)
				BGSoundPlayer.Instance.OpenSound();
			else
				BGSoundPlayer.Instance.CloseSound();


			PlayerPrefs.SetInt("SoundBG", isOn ? 0 : 1);
		}

		public void OnSoundEffectClick(bool isOn)
		{
			PlayerPrefs.SetInt("SoundEffect", isOn ? 0 : 1);
		}
	}
}
