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

	public class RolePanel : BasePanel
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
		GameObject _rechargeBtn;

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);

			BGSoundPlayer.Instance.PlayMainSound();

			_nameTxt.text = Save.Player.NickName;
			_icon.sprite = Save.Player.Settings.Icon;
			_levelTxt.text = "Lv." + (int)Save.Player.Level;
			_levelSlider.value = Save.Player.Level - Mathf.FloorToInt(Save.Player.Level);

			_rechargeBtn.SetActive(!Save.Player.IsGetGift && Account.IsGift());
		}

		public void OnRankClick()
		{
			MainUI.Instance.ShowPanel<RankingPanel>();
		}

		public void OnTalentClick()
		{
			MainUI.Instance.ShowPanel<TalentPanel>();
		}

		public void OnBeginGameClick()
		{
			HidePanel();
			GameRequestHelper.BattleStart (delegate(List<BattlePlayerInfo> players) {
				BattleScene.Instance.BeginBattle (players);
			});
		}

		public void OnSettingsClick()
		{
            MainUI.Instance.ShowPanel<SettingsPanel>();
        }

		public void OnRoleDetailsClick()
		{
			MainUI.Instance.ShowPanel<RoleDetailsPanel>();
		}

		public void OnGiftClick()
		{
			MainUI.Instance.ShowPanel<GiftPanel>();
		}
	}

}