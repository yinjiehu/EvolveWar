using UnityEngine;
using System.Collections;
using WhiteCat.FSM;
using System;

public class FindFoodState : AIState
{

	Vector3 _targetPosition;
	Transform _unitArrow;

	bool _isFollowUp = false;
	BattleUnit _followUpTarget;

	float _elapsedTime;

	public override void OnEnter()
	{
		var food = BattleScene.Instance.FoodMgr.GetRandomFood();
		_targetPosition = food.transform.position;
		_unit.SetMoveDierection(_targetPosition - _unit.transform.position);

	}

	public override void OnExit()
	{

	}

	public override void OnUpdate(float deltaTime)
	{
		if(!_isFollowUp)
		{
			if (Vector3.Distance(_unit.transform.position, _targetPosition) < 0.1f)
			{
				if (_nextState != null)
				{
					_fsm.currentState = _nextState;
				}
			}

			var currentPoint = new Vector2(_unit.transform.position.x, _unit.transform.position.y);
			var colliders = Physics2D.OverlapCircleAll(currentPoint,3f);
			for (int i = 0; i < colliders.Length; i++)
			{
				var current = colliders[i];

				var currentRangeUnit = current.transform.GetComponent<BattleUnit>();
				if (currentRangeUnit != null && currentRangeUnit != _followUpTarget && currentRangeUnit.UnitInfo.Uid != _unit.UnitInfo.Uid)
				{
					_followUpTarget = currentRangeUnit;
					_isFollowUp = true;
					_elapsedTime = 0;
				}
			}

		}else
		{
			_elapsedTime += Time.deltaTime;

			if(_followUpTarget != null)
			{
				_unit.SetMoveDierection(_followUpTarget.transform.position - _unit.transform.position);
				_followUpTarget = null;
			}
			if (_elapsedTime > 1.5f)
			{
				_isFollowUp = false;
				_unit.SetMoveDierection(_targetPosition - _unit.transform.position);
			}
		}

		
	}
}
