using UnityEngine;
using System.Collections;
using Kit.UI;
using UnityEngine.UI;
using UI.Main;
using Shared.Battle;
using Network.Code;
using EvolveWar.RequestHelper;

namespace UI.Battle
{
	public class BattleResultPanel : BasePanel
	{
		[SerializeField]
		GameObject _victoryObj;
		[SerializeField]
		GameObject _failureObj;
		[SerializeField]
		Text _totalExpTxt;
		[SerializeField]
		Text _resultExpTxt;
		[SerializeField]
		Text _rankExpTxt;
		[SerializeField]
		Text _killExpTxt;

		[SerializeField]
		EffectSoundPlayer _winSoundPlayer;

		[SerializeField]
		EffectSoundPlayer _loseSoundPlayer;
		bool _victory = false;

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);

            _victory = (bool)obj;
			if(PlayerPrefs.GetInt("SoundEffect", 0) == 0)
			{
				if (_victory)
					_winSoundPlayer.PlaySound();
				else
					_loseSoundPlayer.PlaySound();
			}
			_victoryObj.SetActive(_victory);
			_failureObj.SetActive(!_victory);
			//var exp = 30f * (victory ? 1 : 0.5f) + (22 - 2 * BattleScene.Instance.GetRankByUid(Save.Player.UID) + 1.5f * BattleScene.Instance.GetKillNumByUid(Save.Player.UID));
			var resultExp = 30f * (_victory ? 1 : 0.5f);
			var rankExp = 22 - 2 * BattleScene.Instance.GetRankByUid(Save.Player.UID);
			var killExp = 1.5f * BattleScene.Instance.GetKillNumByUid(Save.Player.UID);
			_totalExpTxt.text = "本局获得总经验值：+" + (int)(resultExp + rankExp + killExp);
			_resultExpTxt.text = (_victory ? "胜利：+" : "失败：+") + (int)resultExp;
			_rankExpTxt.text = "排行：+" + (int)rankExp;
			_killExpTxt.text = "击杀：+" + (int)killExp;

		}

		public void OnReturnClick()
		{
			EffectSoundMgr.Instance.PlayClickSound();
			HidePanel();
			GameRequestHelper.BattleClear (_victory, delegate() {

				BattleScene.Instance.EndBattle ();
				MainUI.Instance.ShowPanel<RolePanel> ();
			});
		}
	}
}