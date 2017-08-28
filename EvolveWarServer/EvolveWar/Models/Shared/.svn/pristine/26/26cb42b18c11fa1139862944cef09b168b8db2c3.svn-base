using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Model;
using Network.Code;
using Models.Battle;

namespace Shared.Battle
{
	public class BattleStartRes
	{
		public CodeMessage Result { set; get; }

		public List<BattlePlayerInfo> Players;

		public BattleStartRes()
		{
			Result = ReturnCode.OK;
		}

		public BattleStartRes(CodeMessage code)
		{
			Result = code;
		}
	}
	
}