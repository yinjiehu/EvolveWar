using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EvolveWar.Actions
{
	public class IdleAction : FsmStateAlibity
	{
		public override void OnEnter()
		{
			base.OnEnter();
			Finish();
		}
	}
}
