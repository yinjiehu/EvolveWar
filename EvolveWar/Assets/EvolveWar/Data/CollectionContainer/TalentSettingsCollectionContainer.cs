using Data.Talent;
using Kit.Inspector;
using Kit.Utility.OpenXml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentSettingsCollectionContainer : MonoBehaviour
{

	[SerializeField]
	TextAsset _xml;

	[SerializeField]
	TalentSettingsCollection _collection = new TalentSettingsCollection();
	public TalentSettingsCollection DataCollection { get { return _collection; } }



#if UNITY_EDITOR
	[SerializeField]
	InspectorButton _loadXml;
	public void LoadXml()
	{
		var xmlParser = new OpenXmlParser();
		xmlParser.LoadXml(_xml.text);
		var sheet = xmlParser.SelectSheet("Settings");
		_collection.DataCollection = new List<TalentSettings>();
		sheet.SelectRow(1);
		while (sheet.MoveNext())
		{
			var currentRow = sheet.CurrentRow;
			var id = currentRow[0].StringValue;
			var iconName = currentRow[1].StringValue;
			var level = currentRow[2].IntValue;
			var levelSettings = currentRow[3].IntValue;
			var displayName = currentRow[4].StringValue;
			var description = currentRow[5].StringValue;
			var attack = currentRow[6].IntValue;
			var defense = currentRow[7].IntValue;
			var life = currentRow[8].IntValue;
			var crit = currentRow[9].FloatValue;
			var critHurt = currentRow[10].IntValue;
			var ignoreAttack = currentRow[11].IntValue;
			var ignoreDefense = currentRow[12].IntValue;
			var settings = new TalentSettings();
			settings.ID = id;
			settings.Icon = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Assets/EvolveWar/Texture2D/Talent/" + iconName + ".png");
			settings.Level = level;
			settings.LevelSettings = levelSettings;
			settings.DisplayName = displayName;
			settings.Description = description;
			settings.Attack = attack;
			settings.Defense = defense;
			settings.Life = life;
			settings.Crit = crit;
			settings.CritHurt = critHurt;
			settings.IgnoreAttack = ignoreAttack;
			settings.IgnoreDefense = ignoreDefense;
			_collection.DataCollection.Add(settings);
		}
	}


	[SerializeField]
	InspectorButton _serializeToJson;

	public void SerializeToJson()
	{
		Kit.Utility.UtilitySerializeHelper.WriteToFile(DataCollection, "TalentSettings");
	}

#endif
}
