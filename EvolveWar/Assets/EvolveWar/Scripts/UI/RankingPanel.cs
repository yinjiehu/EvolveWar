using UnityEngine;
using System.Collections;
using Kit.UI;
using Shared.Rank;
using EvolveWar.RequestHelper;
using UnityEngine.UI;

namespace UI.Main
{

	public class RankingPanel : BasePanel
	{
		[SerializeField]
		RankBlock myBlock;

		[SerializeField]
		ListMemberUpdater _updater;

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);

			EffectSoundMgr.Instance.PlayOpenPanelSound();
			GameRequestHelper.GetRankingList(delegate(System.Collections.Generic.List<RankingInfo> list) {


				list.Reverse();


				var idx = list.FindIndex(r => r.DisplayName == Save.Player.NickName);
				if(idx != -1)
				{
					myBlock.UpdateData(idx + 1, list[idx]);
				}else
				{
					myBlock.UpdateData(idx + 1, null);
				}
				int index = 1;
				_updater.OnListUpdate<RankingInfo>(list, delegate (RankingInfo info, GameObject block)
					{
						block.GetComponent<RankBlock>().UpdateData(index, info);
						index++;
					});
			});
		}

		public override void HidePanel()
		{
			base.HidePanel();

			EffectSoundMgr.Instance.PlayOpenPanelSound();
		}
	}
}