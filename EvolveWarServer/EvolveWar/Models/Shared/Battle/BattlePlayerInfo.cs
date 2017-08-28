using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Model;
using Network.Code;

namespace Models.Battle
{
	public class BattlePlayerInfo
	{
		public string Uid { set; get; }
		public string NickName { set; get; }
		public int Level { set; get; }
	}
	
}