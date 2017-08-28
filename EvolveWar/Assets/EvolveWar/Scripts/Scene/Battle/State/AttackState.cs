using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
	[SerializeField]
	float _attackInterval = 1f;

	float _elapsedTime;

	public override void OnEnter()
	{
		base.OnEnter();
		_unit.Attack();
		_elapsedTime = 0;
		CancelInvoke("CheckSwitchState");
		Invoke("CheckSwitchState", 0.5f);
	}

	public override void OnUpdate(float deltaTime)
	{
		base.OnUpdate(deltaTime);


		_elapsedTime += deltaTime;
		if(_elapsedTime > _attackInterval)
		{
			_elapsedTime = 0;
			_unit.Attack();
			CancelInvoke("CheckSwitchState");
			Invoke("CheckSwitchState", 0.5f);
		}
	}

	void CheckSwitchState()
	{
		var hits = Physics2D.RaycastAll(_unit.transform.position, (-_unit.transform.position).normalized, _unit.UnitInfo.Range);
		bool isContinueMove = true;
		foreach (var hit in hits)
		{
			var checkUnit = hit.transform.GetComponent<BattleUnit>();
			if (checkUnit != null && checkUnit.UnitInfo.UnitID != _unit.UnitInfo.UnitID)
			{
				isContinueMove = false;
			}
		}
		if (isContinueMove)
		{
			if (_nextState != null)
				_fsm.currentState = _nextState;
		}
	}
}
