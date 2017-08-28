using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Model;
using Network.Code;

namespace Shared.Battle
{

	public class BattleClearRes
	{
		public CodeMessage Result { set; get; }

		public PlayerStatus Player { set; get; }

		public BattleClearRes()
		{
			Result = ReturnCode.OK;
		}

		public BattleClearRes(CodeMessage code)
		{
			Result = code;
		}
	}
	
}