using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Network.Code;
using Model;

namespace Shared.Rank
{

	public class RechargeRes
	{
		public CodeMessage Result { set; get; }

		public PlayerStatus Player { set; get; }

		public RechargeRes()
		{
			Result = ReturnCode.OK;
		}

		public RechargeRes(CodeMessage code)
		{
			Result = code;
		}
	}
	
}