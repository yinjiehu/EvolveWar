using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

namespace EvolveWar.Actions
{
	public class SearchAttackAction : FsmStateAlibity
	{
		float _searchInterval = 3;

		float _elapsedTime;

		BattleUnit _attackTarget;


		public override void OnEnter()
		{
			base.OnEnter();
			_elapsedTime = 0;

			_attackTarget = BattleScene.Instance.GetNearestUnit(_unit.UnitInfo.Uid, _unit.transform.position);
			var direction = _attackTarget.transform.position - _unit.transform.position;
			_unit.SetMoveDierection(direction);
		}

		public override void OnUpdate()
		{
			base.OnUpdate();


			_elapsedTime += Time.deltaTime;
			if (_elapsedTime > _searchInterval)
			{
				_unit.Fsm.SendEvent("FindFood");
			}
		}
	}
}