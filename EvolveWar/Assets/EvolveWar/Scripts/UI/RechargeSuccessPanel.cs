using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kit.UI;
using UI.Main;

public class RechargeSuccessPanel : BasePanel
{
	public override void ShowPanel(object obj)
	{
		base.ShowPanel(obj);
		MainUI.Instance.GetPanel<RolePanel>().ShowPanel(null);
	}
}
