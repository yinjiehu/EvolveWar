using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Data.Talent
{

	[Serializable]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TalentSettings
	{
		[SerializeField]
		[JsonProperty]
		string _id;
		public string ID { set { _id = value; } get { return _id; } }
		public Sprite Icon;
		[JsonProperty]
		[SerializeField]
		string _displayName;
		public string DisplayName { set { _displayName = value; } get { return _displayName; } }
		[JsonProperty]
		[SerializeField]
		int _level;
		public int Level { set { _level = value; } get { return _level; } }
		[JsonProperty]
		[SerializeField]
		int _levelSettings;
		public int LevelSettings { set { _levelSettings = value; } get { return _levelSettings; } }
		[JsonProperty]
		[SerializeField]
		string _description;
		public string Description { set { _description = value; } get { return _description; } }
		[JsonProperty]
		[SerializeField]
		int _attack;
		public int Attack { set { _attack = value; } get { return _attack; } }
		[JsonProperty]
		[SerializeField]
		int _defense;
		public int Defense { set { _defense = value; } get { return _defense; } }
		[JsonProperty]
		[SerializeField]
		int _life;
		public int Life { set { _life = value; } get { return _life; } }
		[JsonProperty]
		[SerializeField]
		float _crit;
		public float Crit { set { _crit = value; } get { return _crit; } }
		[JsonProperty]
		[SerializeField]
		int _critHurt;
		public int CritHurt { set { _critHurt = value; } get { return _critHurt; } }
		[JsonProperty]
		[SerializeField]
		int _ignoreAttack;
		public int IgnoreAttack { set { _ignoreAttack = value; } get { return _ignoreAttack; } }
		[JsonProperty]
		[SerializeField]
		int _ignoreDefense;
		public int IgnoreDefense { set { _ignoreDefense = value; } get { return _ignoreDefense; } }
	}

}