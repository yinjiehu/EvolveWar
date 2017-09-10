using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
public abstract class FsmStateAlibity : FsmStateAction, IAbility
{
	protected BattleUnit _unit;

	public virtual void Init(BattleUnit unit)
	{
		_unit = unit;
	}

	public virtual void OnUpdate(float deltaTime)
	{
	}

	public virtual void LateUpdate()
	{
	}
}
