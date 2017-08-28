using UnityEngine;
using System.Collections;
using Model;
using Newtonsoft.Json;
using System;

public class Save : MonoBehaviour
{
	private static Save _instance;

	public static Save Get()
	{
		if(_instance == null)
		{
			var go = new GameObject("Save");
			_instance = go.AddComponent<Save>();
		}
		return _instance;
	}

	void Awake()
	{
		_instance = this;
	}

	[SerializeField]
	PlayerStatus _player;
	public static PlayerStatus Player { get { return Get()._player; } }

	public const string LOCAL_DATA = "LOCAL_DATA";

	public static void SetPlayer(PlayerStatus p)
	{
		Get()._player = p;
		Get().SaveData();
	}

	public void LoadData()
	{
		var str = PlayerPrefs.GetString(LOCAL_DATA);
		var p = JsonConvert.DeserializeObject<PlayerStatus>(str);
		if(p == null)
		{
			p = new PlayerStatus();
		}
		_player = p;
	}

	public void SaveData()
	{
		var str = JsonConvert.SerializeObject(_player);
		PlayerPrefs.SetString(LOCAL_DATA, str);
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("Data/DeletePlayerPrefs")]
	static void DeletePlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}

#endif

}
