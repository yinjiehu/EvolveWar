using UnityEngine;
using System.Collections;
using Kit.UI;
using UnityEngine.UI;
using EvolveWar.RequestHelper;

namespace UI.Main
{

	public class NickNamePanel : BasePanel
	{
		[SerializeField]
		InputField _nickName;

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);
			_nickName.text = BattleScene.Instance.GetRandomNickName();
		}

		public void OnRandomClick()
		{
			_nickName.text = BattleScene.Instance.GetRandomNickName();
		}

		public void OnCreatePlayerClick()
		{
			if(string.IsNullOrEmpty(_nickName.text))
			{
				return;
			}

			EffectSoundMgr.Instance.PlayClickSound();
			GameRequestHelper.SetNickName (_nickName.text, delegate {

				HidePanel();
				MainUI.Instance.ShowPanel<RolePanel>();
			});
			//Save.Player.SetNickName(_nickName.text);
			//Save.Get().SaveData();
			//MainUI.Instance.ShowPanel<RolePanel>();
		}
	}

}