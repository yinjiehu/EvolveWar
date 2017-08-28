using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Network.Code;
using Model;
namespace Shared.Login
{
	[Serializable]
	public class SetNickNameRes
	{
		public CodeMessage Result;

		public PlayerStatus Player;

		public SetNickNameRes()
		{
			Result = ReturnCode.OK;
		}

		public SetNickNameRes(CodeMessage code)
		{
			Result = code;
		}
	}
}