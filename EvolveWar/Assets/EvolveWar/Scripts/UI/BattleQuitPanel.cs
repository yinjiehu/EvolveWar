using UnityEngine;
using System.Collections;
using Kit.UI;

namespace UI.Battle
{
	public class BattleQuitPanel : BasePanel
	{
		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);
			EffectSoundMgr.Instance.PlayOpenPanelSound();
		}

		public void OnCancelClick()
		{
			BattleScene.Instance.Resume();
			EffectSoundMgr.Instance.PlayOpenPanelSound();
			HidePanel();
		}

		public void OnSureClick()
		{
			HidePanel();
			EffectSoundMgr.Instance.PlayOpenPanelSound();
			MainUI.Instance.ShowPanel<BattleResultPanel>(false);
		}
	}
}