using Kit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main
{
	public class RevivePanel : BasePanel
	{
		[SerializeField]
		Text _countDownTxt;

		float _elapsedTime;

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);
			_elapsedTime = 10;
		}

		void Update()
		{
			_elapsedTime -= Time.deltaTime;
			_countDownTxt.text = string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(_elapsedTime / 60f), (int)_elapsedTime % 60);
			if (_elapsedTime < 0)
			{
				_elapsedTime = 10;
				HidePanel();
				BattleScene.Instance.GeneratePlayer();
			}
			
		}
	}
}