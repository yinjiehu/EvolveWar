using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Model.Talent;
using Data.Talent;
using EvolveWar.RequestHelper;

namespace UI.Main
{
	public class TalentBlock : MonoBehaviour
	{
		[SerializeField]
		Text _levelDescTxt;
		[SerializeField]
		Image _icon;
		[SerializeField]
		Text _nameTxt;
		[SerializeField]
		Text _levelTxt;
		[SerializeField]
		Text _descTxt;
		[SerializeField]
		GameObject _showLevelUp;
		[SerializeField]
		GameObject _showLevelUpLock;

		TalentStatus _status;

		public void UpdateData(TalentStatus status)
		{
			_status = status;
			_levelDescTxt.text = status.Settings.Level + "Level Talent";
			_icon.sprite = status.Settings.Icon;
			_nameTxt.text = status.Settings.DisplayName;

			_levelTxt.text = "Lv." + status.Level;
			bool isShow = Save.Player.Talent.Exp > 0 && Save.Player.Level >= status.Settings.Level && status.Level < 10;
			_showLevelUp.SetActive(isShow);
			_showLevelUpLock.SetActive(!isShow);
			_descTxt.text = status.Settings.Description;
			if(status.Level == 10)
			{
				_showLevelUpLock.SetActive(false);
			}
		}

		public void OnLevelUpClick()
		{
			EffectSoundMgr.Instance.PlayTalentLevelUpSound();
			GameRequestHelper.TalentLevelUp(_status.ID);
		}
	}
}
