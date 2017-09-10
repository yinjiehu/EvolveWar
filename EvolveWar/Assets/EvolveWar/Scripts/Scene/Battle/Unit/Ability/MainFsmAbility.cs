using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EvolveWar.Actions
{
	public class MainFsmAbility : Ability
	{
		[SerializeField]
		PlayMakerFSM _mainFsm;

		public override void Init(BattleUnit unit)
		{
			base.Init(unit);
			_unit.Fsm = _mainFsm;
		}

		public override void OnUpdate(float deltaTime)
		{
			base.OnUpdate(deltaTime);
		}
	}
}
