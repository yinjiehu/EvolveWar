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
//		
		#if UNITY_EDITOR
		GameRequestHelper.Recharge();
		#else
		PayMgr.Instance.BuyProductClick("evolvewarengift");
		#endif
		HidePanel();
	}
}
