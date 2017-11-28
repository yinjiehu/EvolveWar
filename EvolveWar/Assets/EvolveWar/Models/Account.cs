using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account : MonoBehaviour {

	public const string ACCOUNT_STR = "Account";
	public const string GIFT_STR = "Gift";

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

	public static void LoadGift()
	{
		var value = PlayerPrefs.GetInt (GIFT_STR, 0);
		if (value == 0) {
			value = 1;
		}
		PlayerPrefs.SetInt (GIFT_STR, value);
	}

	public static bool IsGift()
	{
		return PlayerPrefs.GetInt (GIFT_STR, 0) == 0;
	}
}
