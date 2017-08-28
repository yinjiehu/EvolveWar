using Kit.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAttackState : AIState
{
	[SerializeField]
	float _searchInterval = 3;

	float _elapsedTime;

	BattleUnit _attackTarget;

	public override void OnEnter()
	{
		base.OnEnter();
		_elapsedTime = 0;

		_attackTarget = BattleScene.Instance.GetNearestUnit(_unit.UnitInfo.Uid, _unit.transform.position);
	}

	public override void OnUpdate(float deltaTime)
	{
		base.OnUpdate(deltaTime);
		_elapsedTime += Time.deltaTime;
		if(_elapsedTime > _searchInterval)
		{
			_elapsedTime = 0;

			if (_attackTarget != null)
			{
				var direction = _attackTarget.transform.position - _unit.transform.position;
				_unit.SetMoveDierection(direction);
				var hits = Physics2D.RaycastAll(_unit.transform.position, _unit.MoveDirection.normalized, _unit.UnitInfo.Range / 2);
				foreach (var hit in hits)
				{
					var checkUnit = hit.transform.GetComponent<BattleUnit>();
					if (checkUnit != null && checkUnit.UnitInfo.UnitID != _unit.UnitInfo.UnitID)
					{
						if(_previousState != null)
						{
							_fsm.currentState = _previousState;
						}
					}
				}
			}
			else
			{
				if (_nextState != null)
				{
					_fsm.currentState = _nextState;
				}
			}
		}
	}
}
