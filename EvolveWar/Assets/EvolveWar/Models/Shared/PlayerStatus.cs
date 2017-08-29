﻿using UnityEngine;
using System.Collections;
using System;
using Model.Talent;
using Newtonsoft.Json;
using Data.Talent;
using Data.Player;
using EvolveWar;

namespace Model
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class PlayerStatus
	{
		[SerializeField]
		[JsonProperty]
		string _uid;
		public string UID { get { return _uid; } }

		[SerializeField]
		[JsonProperty]
		int _exp;
		[SerializeField]
		[JsonProperty]
		string _nickName;
		public string NickName { get { return _nickName; } }
		[SerializeField]
		[JsonProperty]
		TalentStatusCollection _talent = new TalentStatusCollection();
		public TalentStatusCollection Talent { get { return _talent; } }

		public void SetUID(string uid)
		{
			_uid = uid;
		}

		public void AddExp(int exp)
		{
			int currentLevel = (int)Level;
			_exp += exp;
			if(currentLevel != (int)Level)
			{
				_talent.AddExp();
			}
		}

		public void SetExp(int exp)
		{
			_exp = exp;
		}

		public float Level { get { return _exp / 100f; } }

		public void SetNickName(string nickname)
		{
			_nickName = nickname;
		}

		public void Initialize(TalentSettingsCollection collection)
		{
			_talent = new TalentStatusCollection();
			foreach (var setting in collection.DataCollection)
			{
				if(!_talent.Contains(setting.ID))
					_talent.AddStatus(setting);
			}
		}

		public void Recharge()
		{
			SetExp(30 * 100);
			_talent.TalentReset();
			_talent.SetExp(30);
		}

		public PlayerSettings Settings
		{
			get
			{
				var tempLevel = Math.Floor(Level / 5f);
				if (tempLevel < 1)
					tempLevel = 1;
				
				return Config.PlayerSettings.Get((int)tempLevel);
			}
		}
	}
}