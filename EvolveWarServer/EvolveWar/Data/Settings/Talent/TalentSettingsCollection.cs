using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Data.Talent
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class TalentSettingsCollection
	{

		[SerializeField]
		[JsonProperty]
		List<TalentSettings> _collection = new List<TalentSettings>();
		public List<TalentSettings> DataCollection { get { return _collection; } set { _collection = value; } }

		public TalentSettings Get(string id)
		{
			var index = _collection.FindIndex(t => t.ID == id);
			if (index < 0)
				return null;
			return _collection[index];
		}

	}
}
