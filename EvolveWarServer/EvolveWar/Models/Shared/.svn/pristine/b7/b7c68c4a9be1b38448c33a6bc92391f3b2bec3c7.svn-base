using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Network.Code;
using Model;
namespace Shared.Login
{
	[Serializable]
	public class LoginRes
	{

		public CodeMessage Result;

		public string AccountID;

		public PlayerStatus Player;

		public LoginRes()
		{
			Result = ReturnCode.OK;
		}

		public LoginRes(CodeMessage code)
		{
			Result = code;
		}
	}

}