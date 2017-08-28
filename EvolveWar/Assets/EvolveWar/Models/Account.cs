using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account : MonoBehaviour {

	public const string ACCOUNT_STR = "Account";

	public static string PlayerID = "";

	public static void LoadAccount()
	{
		var str = PlayerPrefs.GetString(ACCOUNT_STR);

		PlayerID = str.ToString();
		if (string.IsNullOrEmpty(PlayerID))
		{
			PlayerID = Guid.NewGuid().ToString();
		}
		SaveAccount();
	}
	
	public static void SaveAccount()
	{
		if (string.IsNullOrEmpty(PlayerID))
		{
			PlayerID = Guid.NewGuid().ToString();
		}
		PlayerPrefs.SetString(ACCOUNT_STR, PlayerID);
	}
}
