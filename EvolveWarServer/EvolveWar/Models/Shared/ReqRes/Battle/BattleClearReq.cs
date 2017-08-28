using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Model;
using Network.Code;

namespace Shared.Battle
{
	public class BattleClearReq
	{
		public bool Result { set; get; }
		public string TempID { set; get; }
		public string Uid { set; get; }
		public int Rank { set; get; }
		public int Kill { set; get; }
		public int Dead { set; get; }
	}
	
}