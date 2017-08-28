using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Data.Talent;
using System.Linq;

namespace Model.Talent
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class TalentStatusCollection
	{
		[SerializeField]
		[JsonProperty]
		List<TalentStatus> _collection = new List<TalentStatus>();
		public List<TalentStatus> DataCollection { get { return _collection; } }

		[SerializeField]
		[JsonProperty]
		int _exp;
		public int Exp { get { return _exp; } }

		public TalentStatus Get(string id)
		{
			var index = _collection.FindIndex(t => t.ID == id);
			if (index < 0)
				return null;
			return _collection[index];
		}

		public void AddExp()
		{
			_exp += 1;
		}

		public void SetExp(int exp)
		{
			_exp = exp;
		}

		public void AddStatus(TalentSettings settings)
		{
			var status = new TalentStatus();
			status.SetID(settings.ID);
			status.SetLevel(0);
			status.SetExp(0);
			_collection.Add(status);
		}

		public void LevelUp(string id)
		{
			if(_exp > 0)
			{
				var talentStatus = Get(id);
				if (talentStatus != null && talentStatus.Level < 10)
				{
					_exp -= 1;
					talentStatus.AddExp(1);
				}
					
			}
		}

		public void TalentReset()
		{
			var levels = _collection.Select(t => t.Level);
			var totalExp = _exp + levels.Sum();
			foreach(var status in _collection)
			{
				status.SetExp(0);
			}
			SetExp(totalExp);
		}
	}
}