using UnityEngine;
using System.Collections;
using WhiteCat.FSM;
using System;
using Kit.Utility;

public class BypassState : AIState
{

	public override void OnEnter()
	{
		base.OnEnter();
	}

	public override void OnUpdate(float deltaTime)
	{
		bool canSwitchState = true;
		var hits = Physics2D.RaycastAll(_unit.transform.position, _unit.MoveDirection.normalized, _unit.UnitInfo.Range / 2);
		foreach (var hit in hits)
		{
			var checkUnit = hit.transform.GetComponent<BattleUnit>();
			if (checkUnit != null && checkUnit.UnitInfo.UnitID != _unit.UnitInfo.UnitID)
			{
				var currDirection = _unit.MoveDirection + new Vector3(0.2f, 0, 0);
				_unit.SetMoveDierection(currDirection);
				canSwitchState = false;
			}
		}
		if(canSwitchState)
		{
			if(_nextState != null)
			{
				_fsm.currentState = _nextState;
			}
		}
	}
}
