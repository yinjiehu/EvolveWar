using UnityEngine;
using System.Collections;
using Kit.UI;
using Shared.Rank;
using UnityEngine.UI;
using System;

namespace UI.Main
{
	public class DialogPanel : BasePanel
	{
		[SerializeField]
		Text _content;

		Action _callback;

		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);
		}

		public void OnSureClick()
		{
			if(_callback != null)
			{
				_callback();
			}
		}

		public void ShowPanel(string message, Action onCallback)
		{
			_content.text = message;
			_callback = onCallback;
		}

		public static void ShowDialogMessage(string message, Action onConfirm=null)
		{
			MainUI.Instance.ShowPanel<DialogPanel>().ShowPanel(message, onConfirm);
		}
	}
}