using Kit.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveAbility : Ability
{

	public override void Init(BattleUnit unit)
	{
		base.Init(unit);
	}

	public override void OnUpdate(float deltaTime)
	{
		base.OnUpdate(deltaTime);
		var direction = _unit.MoveDirection.normalized;

		_unit.transform.Translate(direction * deltaTime * 3);

		var unitArrow = _unit.GetRoleArrow();
		if (unitArrow != null)
		{
			float angle = Vector3.Angle(Vector3.up, direction);
			if (direction.x > 0)
			{
				angle = -angle;
			}
			Vector3 axis = Vector3.zero;
			unitArrow.localRotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
