using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;

namespace Data.Skill
{

	[Serializable]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SkillSettings
	{
		[JsonProperty]
		string _id;
		public string ID;
		public Sprite Icon;
		[JsonProperty]
		string _displayName;
		public string DisplayName;
		[JsonProperty]
		int _quality;
		public int Quality;
		[JsonProperty]
		int _level;
		public int Level;
		[JsonProperty]
		string _description;
		public string Description;
		[JsonProperty]
		int _attack;
		public int Attack;
		[JsonProperty]
		int _defense;
		public int Defense;
		[JsonProperty]
		int _life;
		public int Life;
		[JsonProperty]
		float _crit;
		public float Crit;
		[JsonProperty]
		int _critHurt;
		public int CritHurt;
		[JsonProperty]
		int _ignoreAttack;
		public int IgnoreAttack;
		[JsonProperty]
		int _ignoreDefense;
		public int IgnoreDefense;
		[JsonProperty]
		int _triggerProb;
		public int TriggerProb;
		[JsonProperty]
		int _hurt;
		public int Hurt;
	}

}