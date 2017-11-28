using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using EvolveWar.RequestHelper;

public class PayMgr : MonoBehaviour {

	static PayMgr _instance;


	public static PayMgr Instance
	{
		get { return _instance; }
	}

	private void Awake()
	{
		_instance = this;
	}

	[DllImport("__Internal")]
	private static extern void InitIAPManager();//初始化

	[DllImport("__Internal")]
	private static extern bool IsProductAvailable();//判断是否可以购买

	[DllImport("__Internal")]
	private static extern void RequstProductInfo(string s);//获取商品信息

	[DllImport("__Internal")]
	private static extern void BuyProduct(string s);//购买商品

	[DllImport("__Internal")]
	private static extern string getIPv6(string mHost, string mPort);  

	[DllImport("__Internal")]
	private static extern void callIOSSetting();  

	//获取product列表
	void ShowProductList(string s)
	{
		
		Debug.Log("获取product列表:" + s);
	}

	//获取商品回执
	void ProvideContent(string s)
	{
		Debug.Log("获取商品回执 : " + s);
		GameRequestHelper.Recharge ();
	}

	void Start()
	{
		Debug.Log("初始化ios内购信息");
		InitIAPManager();
	}

	public void CallIosSetting()
	{
		callIOSSetting ();
	}

	public string GetIPv6(string mHost, string mPort)
	{
		return getIPv6 (mHost, mPort);
	}

	/// <summary>
	/// 购买商品
	/// </summary>
	/// <param name="priductid">商品ID</param>
	public void BuyProductClick(string priductid)
	{
		if (!IsProductAvailable())
		{
			Debug.Log("无法购买此商品！请联系客服！");
		}
		else
		{
			//产品id，此处id要和apple开发者后台产品id相同，每个产品id用\t相隔
			RequstProductInfo(priductid);
		}
		BuyProduct(priductid);
	}
}
