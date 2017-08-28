using UnityEngine;
using System.Collections;
using WhiteCat.FSM;
using System;

public class AIState : BaseState
{

	[SerializeField]
	protected AIState _previousState;
	[SerializeField]
	protected AIState _nextState;
	protected BattleUnit _unit;
	protected StateMachine _fsm;

	public virtual void OnInit(BattleUnit unit, StateMachine fsm)
	{
		_unit = unit;
		_fsm = fsm;
	}

	public override void OnEnter()
	{
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate(float deltaTime)
	{
	}
}
