using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Model.Talent;

namespace UI.Main
{
	public class TalentSimpleBlock : MonoBehaviour
	{
		[SerializeField]
		Image _icon;
		[SerializeField]
		Text _nameTxt;
		[SerializeField]
		Text _levelTxt;

		public void UpdateData(TalentStatus status)
		{
			_icon.sprite = status.Settings.Icon;
			_nameTxt.text = status.Settings.DisplayName;
			_levelTxt.text = "Lv." + status.Level;
		}
	}
}
