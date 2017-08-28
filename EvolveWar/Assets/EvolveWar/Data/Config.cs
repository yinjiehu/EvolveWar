using UnityEngine;
using System.Collections;
using Data.Talent;
using Data.Player;
using Data.Skill;

namespace EvolveWar
{
	public class Config : MonoBehaviour
	{

		private static Config _instance;

		public static Config Get()
		{
			if (_instance == null)
			{
				var go = new GameObject("Config");
				_instance = go.AddComponent<Config>();
			}
			return _instance;
		}

		void Awake()
		{
			_instance = this;
		}

		[SerializeField]
		TalentSettingsCollectionContainer _talentSettings;
		public static TalentSettingsCollection TalentSettings { get { return Get()._talentSettings.DataCollection; } }


		[SerializeField]
		SkillSettingsCollection _skillSettings;
		public static SkillSettingsCollection SkillSettings { get { return Get()._skillSettings; } }

		[SerializeField]
		PlayerSettingsCollection _playerSettings;
		public static PlayerSettingsCollection PlayerSettings { get { return Get()._playerSettings; } }
	}
}
