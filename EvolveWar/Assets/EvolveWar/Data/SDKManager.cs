using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDKManager : MonoBehaviour {

	static SDKManager _instance;


	public static SDKManager Instance
	{
		get { return _instance; }
	}

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
#if UNITY_IOS
		TalkingDataGA.OnStart("3FC846F75D2E471394CC0F19B7F4DCCA", "IOS");
#else

		TalkingDataGA.OnStart("3FC846F75D2E471394CC0F19B7F4DCCA", "Android");
#endif
	}

	public void SetAccount()
	{
		TDGAAccount account = TDGAAccount.SetAccount(TalkingDataGA.GetDeviceId());
		account.SetAccountType(AccountType.ANONYMOUS);
	}
}
