using UnityEngine;
using System.Collections;
using Data.Talent;
using System;
using Data.Player;

namespace EvolveWar
{
	public class LoadFromJsonFileAttribute : Attribute
	{
		public string CustomName { set; get; }
	}

	public class Config
	{
		[LoadFromJsonFile]
		public static TalentSettingsCollection TalentSettings { set; get; }


		[LoadFromJsonFile]
		public static PlayerSettingsCollection PlayerSettings { set; get; }
	}

}