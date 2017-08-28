using Kit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI.Main
{

	public class NetWaitPanel : BasePanel
	{
		[SerializeField]
		Transform _content;
		public override void ShowPanel(object obj)
		{
			base.ShowPanel(obj);
			Invoke("ShowContent", 0.5f);
		}

		void OnDisable()
		{
			CancelInvoke("ShowContent");
		}

		void ShowContent()
		{
			if (_content != null)
				_content.gameObject.SetActive(true);
		}

		public static void Show()
		{
			MainUI.Instance.ShowPanel<NetWaitPanel>();
		}

		public static void Hide()
		{
			MainUI.Instance.HidePanel<NetWaitPanel>();
		}
	}
}