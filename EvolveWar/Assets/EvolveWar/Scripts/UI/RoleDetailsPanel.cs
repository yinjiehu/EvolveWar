using UnityEngine;
using System.Collections;
using Kit.UI;
using UnityEngine.UI;
using Model.Talent;
using Network.Code;
using System.Collections.Generic;
using Models.Battle;
using System;
using Kit.Utility;
using EvolveWar.RequestHelper;

namespace UI.Main
{

	public class RoleDetailsPanel : BasePanel
	{
		[SerializeField]
		Image _icon;
		[SerializeField]
		Text _nameTxt;
		[SerializeField]
		Text _levelTxt;
		[SerializeField]
		Slider _levelSlider;
		[SerializeField]
		Text _attackTxt;
		[SerializeField]
		Text _defenseTxt;
		[SerializeField]
		Text _hpTxt;
		[SerializeField]
		Text _ignoreAttackTxt;
		[SerializeField]
		Text _ignoreDefenseTxt;

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);
			_nameTxt.text = Save.Player.NickName;
			var settings = Save.Player.Settings;
			_icon.sprite = settings.Icon;
			_levelTxt.text = "Lv." + (int)Save.Player.Level;
			_levelSlider.value = Save.Player.Level - Mathf.FloorToInt(Save.Player.Level);
			_attackTxt.text = settings.Attack.ToString();
			_defenseTxt.text = settings.Defense.ToString();
			_hpTxt.text = settings.Life.ToString();
			_ignoreAttackTxt.text = settings.IgnoreAttack.ToString();
			_ignoreDefenseTxt.text = settings.IgnoreDefense.ToString();

			EffectSoundMgr.Instance.PlayOpenPanelSound();
		}


		public override void HidePanel()
		{
			base.HidePanel();

			EffectSoundMgr.Instance.PlayOpenPanelSound();
		}
	}

}