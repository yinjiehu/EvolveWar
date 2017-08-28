using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Data.Talent;
using EvolveWar;

namespace Model.Talent
{

	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class TalentStatus
	{
		[SerializeField]
		[JsonProperty]
		string _id;
		public string ID { get { return _id; } }
		[SerializeField]
		[JsonProperty]
		int _level;
		public int Level { get { return _exp; } }
		[SerializeField]
		[JsonProperty]
		int _exp;
		public int Exp { get { return _exp; } }

		public void SetID(string id)
		{
			_id = id;
		}

		public void SetLevel(int level)
		{
			_level = level;
		}

		public void SetExp(int exp)
		{
			_exp = exp;
		}

		public void AddExp(int exp)
		{
			_exp += exp;
		}

		public TalentSettings Settings
		{
			get
			{
				var tempLevel = Level;
				if (tempLevel < 1)
					tempLevel = 1;
				if (tempLevel > 10)
					tempLevel = 10;

				return Config.TalentSettings.Get(_id, tempLevel);
			}
		}
	}

}