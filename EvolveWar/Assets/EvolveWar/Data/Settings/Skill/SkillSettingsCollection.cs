using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Kit.Inspector;
using Kit.Utility.OpenXml;

namespace Data.Skill
{
	[Serializable]
	public class SkillSettingsCollection : MonoBehaviour
	{
		[SerializeField]
		TextAsset _xml;

		[SerializeField]
		List<SkillSettings> _collection;
		public List<SkillSettings> DataCollection { get { return _collection; } }

        public SkillSettings Get(int index)
        {
            if (index > _collection.Count)
                return null;
            return _collection[index];
        }

        public SkillSettings Get(string id)
        {
            var index = _collection.FindIndex(s => s.ID == id);
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
			_collection = new List<SkillSettings>();
			sheet.SelectRow(1);
			while(sheet.MoveNext())
			{
				var currentRow = sheet.CurrentRow;
				var id = currentRow[0].StringValue;
				var iconName = currentRow[1].StringValue;
				var quality = currentRow[2].IntValue;
				var level = currentRow[3].IntValue;
				var displayName = currentRow[4].StringValue;
				var description = currentRow[5].StringValue;
				var attack = currentRow[6].IntValue;
				var defense = currentRow[7].IntValue;
				var life = currentRow[8].IntValue;
				var crit = currentRow[9].FloatValue;
				var critHurt = currentRow[10].IntValue;
				var ignoreAttack = currentRow[11].IntValue;
				var ignoreDefense = currentRow[12].IntValue;
				var triggerProb = currentRow[13].IntValue;
				var hurt = currentRow[14].IntValue;
				var settings = new SkillSettings();
				settings.ID = id;
				settings.Icon = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Assets/EvolveWar/Texture2D/Skill/" + iconName + ".png");
				settings.Quality = quality;
				settings.Level = level;
				settings.DisplayName = displayName;
				settings.Description = description;
				settings.Attack = attack;
				settings.Defense = defense;
				settings.Life = life;
				settings.Crit = crit;
				settings.CritHurt = critHurt;
				settings.IgnoreAttack = ignoreAttack;
				settings.IgnoreDefense = ignoreDefense;
				settings.TriggerProb = triggerProb;
				settings.Hurt = hurt;
				_collection.Add(settings);
			}
		}


		[SerializeField]
		InspectorButton _serializeToJson;

		public void SerializeToJson()
		{
			Kit.Utility.UtilitySerializeHelper.WriteToFile(this, "SkillSettings");
		}


#endif
	}
}
