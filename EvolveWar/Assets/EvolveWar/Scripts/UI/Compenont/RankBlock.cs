using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Shared.Rank;
using EvolveWar;

namespace UI.Main
{
	public class RankBlock : MonoBehaviour
	{
		[SerializeField]
		Sprite NormalBg;
		[SerializeField]
		Sprite MyBg;
		[SerializeField]
		RankNumShow _rankNum;
		[SerializeField]
		Image _icon;
		[SerializeField]
		Text _nameTxt;
		[SerializeField]
		Text _skillTxt;
		[SerializeField]
		Text _deadTxt;

		public void UpdateData(int index, RankingInfo info)
		{
			if(info == null)
			{
				_rankNum.UpdateData(0);
				var tempLevel = Mathf.FloorToInt(Save.Player.Level / 5f);
				if (tempLevel < 1)
					tempLevel = 1;
				_icon.sprite = Config.PlayerSettings.Get(tempLevel).Icon;
				_nameTxt.text = Save.Player.NickName;
				_skillTxt.text = "0";
				_deadTxt.text = "0";
				
				if (info == null || info.DisplayName == Save.Player.NickName)
				{
					GetComponent<Image>().sprite = MyBg;
				}
				else
				{
					GetComponent<Image>().sprite = NormalBg;
				}
			}
			else
			{
				_rankNum.UpdateData(index);
				var tempLevel = Mathf.FloorToInt(info.Level / 5f);
				if (tempLevel < 1)
					tempLevel = 1;
				_icon.sprite = Config.PlayerSettings.Get(tempLevel).Icon;
				_nameTxt.text = info.DisplayName;
				_skillTxt.text = info.Kill.ToString();
				_deadTxt.text = info.Dead.ToString();

				if (info.DisplayName == Save.Player.NickName)
				{
					GetComponent<Image>().sprite = MyBg;
				}
				else
				{
					GetComponent<Image>().sprite = NormalBg;
				}
			}
		}
	}
}
