using UnityEngine;
using System.Collections;
using Kit.UI;
using Data.Talent;
using Model.Talent;
using UnityEngine.UI;
using EvolveWar;
using EvolveWar.RequestHelper;

namespace UI.Main
{

	public class TalentPanel : BasePanel
	{
		[SerializeField]
		ListMemberUpdater _updater;
		[SerializeField]
		Text _talentTxt;

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);

			_talentTxt.text = "天赋点数：" + Save.Player.Talent.Exp;

			var list = Save.Player.Talent.DataCollection;
			_updater.OnListUpdate<TalentStatus>(list, delegate (TalentStatus status, GameObject block)
			{
				block.GetComponent<TalentBlock>().UpdateData(status);
			});
		}

		public void OnResetClick()
		{
			GameRequestHelper.TalentReset();
		}
	}

}