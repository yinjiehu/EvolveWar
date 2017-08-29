﻿using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using Kit.Utility.OpenXml;

namespace Data.Player
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class PlayerSettings
	{
		[JsonProperty]
		[SerializeField]
		int _level;
		[XmlParseIgnore]
		public int Level { get { return _level; } set { _level = value; } }
		[JsonProperty]
		[SerializeField]
		int _attack;
		[XmlParseIgnore]
		public int Attack { get { return _attack; } set { _attack = value; } }
		[JsonProperty]
		[SerializeField]
		int _defense;
		[XmlParseIgnore]
		public int Defense { get { return _defense; } set { _defense = value; } }
		[JsonProperty]
		[SerializeField]
		int _life;
		[XmlParseIgnore]
		public int Life { get { return _life; } set { _life = value; } }
		[JsonProperty]
		[SerializeField]
		float _crit;
		[XmlParseIgnore]
		public float Crit { get { return _crit; } set { _crit = value; } }
		[JsonProperty]
		[SerializeField]
		int _critHurt;
		[XmlParseIgnore]
		public int CritHurt { get { return _critHurt; } set { _critHurt = value; } }
		[JsonProperty]
		[SerializeField]
		int _ignoreAttack;
		[XmlParseIgnore]
		public int IgnoreAttack { get { return _ignoreAttack; } set { _ignoreAttack = value; } }
		[JsonProperty]
		[SerializeField]
		int _ignoreDefense;
		[XmlParseIgnore]
		public int IgnoreDefense { get { return _ignoreDefense; } set { _ignoreDefense = value; } }
		public Sprite Icon;

	}
}