using EvolveWar.RequestHelper;
using Kit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPanel : BasePanel
{

	public override void ShowPanel(object obj)
	{
		base.ShowPanel(obj);
	}

	public void OnRechargeClick()
	{
		GameRequestHelper.Recharge();
		HidePanel();
	}
}
