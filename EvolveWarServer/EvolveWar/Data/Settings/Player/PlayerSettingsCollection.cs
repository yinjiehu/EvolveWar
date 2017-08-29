﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kit.Utility.OpenXml;
using Kit.Inspector;

namespace Data.Player
{

	public class PlayerSettingsCollection : MonoBehaviour
	{
		[SerializeField]
		TextAsset _xml;
		[SerializeField]
		List<PlayerSettings> _collection;
		public List<PlayerSettings> DataCollection { get { return _collection; } }

		public PlayerSettings Get(int level)
		{
			var index = _collection.FindIndex(p => p.Level == level);
			if (index < 0)
				return null;
			return _collection[index];
		}
#if UNITY_EDITOR
		[SerializeField]
		InspectorButton _loadXml;
		public void LoadXml()
		{
			var xmlParser = new OpenXmlParser();
			xmlParser.LoadXml(_xml.text);
			var sheet = xmlParser.SelectSheet("Settings");
			_collection = new List<PlayerSettings>();
			sheet.SelectRow(2);
			while (sheet.MoveNext())
			{
				var currentRow = sheet.CurrentRow;
				var level = currentRow[0].IntValue;
				var attack = currentRow[1].IntValue;
				var defense = currentRow[2].IntValue;
				var life = currentRow[3].IntValue;
				var crit = currentRow[4].FloatValue;
				var critHurt = currentRow[5].IntValue;
				var ignoreAttack = currentRow[6].IntValue;
				var ignoreDefense = currentRow[7].IntValue;
				var iconName = currentRow[8].StringValue;
				var settings = new PlayerSettings();
				settings.Level = level;
				settings.Attack = attack;
				settings.Defense = defense;
				settings.Life = life;
				settings.Crit = crit;
				settings.CritHurt = critHurt;
				settings.IgnoreAttack = ignoreAttack;
				settings.IgnoreDefense = ignoreDefense;
				settings.Icon = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Assets/EvolveWar/Texture2D/BattleIcon/" + iconName + ".psd");
				_collection.Add(settings);
			}
		}
		[SerializeField]
		InspectorButton _serializeToJson;

		public void SerializeToJson()
		{
			Kit.Utility.UtilitySerializeHelper.WriteToFile(this, "PlayerSettings");
		}

#endif

	}
}