using UnityEngine;
using System.Collections;
using Kit.UI;
using Network.Code;
using EvolveWar.RequestHelper;

namespace UI.Main
{
	public class LoginPanel : BasePanel
	{

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);
			BGSoundPlayer.Instance.PlayLoginSound();
			Account.LoadAccount();
		}

		public void OnBeginGameClick()
		{
			EffectSoundMgr.Instance.PlayClickSound();
			GameCenterManager.Instance.GameCenterLogin (GameCenterCallback);

		}

		void GameCenterCallback(string gcid)
		{
			GameRequestHelper.Login (gcid, delegate {
				HidePanel();
				if(string.IsNullOrEmpty(Save.Player.NickName))
				{
					MainUI.Instance.ShowPanel<NickNamePanel>();
				}else
				{
					MainUI.Instance.ShowPanel<RolePanel>();
				}
			});
		}

		public void OnSettingsClick()
		{
			MainUI.Instance.ShowPanel<SettingsPanel>();
		}
	}
}
