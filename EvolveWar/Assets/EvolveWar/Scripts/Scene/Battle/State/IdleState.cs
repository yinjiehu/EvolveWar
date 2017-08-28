using UnityEngine;
using System.Collections;
using WhiteCat.FSM;
using System;

public class IdleState : AIState
{
	public override void OnEnter()
	{
		
	}


	public override void OnExit()
	{
	}

	public override void OnUpdate(float deltaTime)
	{
		if (_nextState != null)
		{
			_fsm.currentState = _nextState;
		}
	}
}
