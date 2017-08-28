using Kit.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveAbility : Ability
{
	FollowCamera _followCamera;

	public override void Init(BattleUnit unit)
	{
		base.Init(unit);
		_followCamera = GameObject.FindObjectOfType<FollowCamera>();
	}

	public override void OnUpdate(float deltaTime)
	{
		base.OnUpdate(deltaTime);
		var direction = _unit.MoveDirection.normalized;

		var deltaPosition = direction * deltaTime * 3;
		var movePosition = _unit.transform.position + new Vector3(deltaPosition.x, deltaPosition.y, 0);
		Vector3 targetDirection = Vector3.zero;

		float width = (_followCamera.MapSize.x - _unit.UnitSize.x) / 2f;
		float height = (_followCamera.MapSize.y - _unit.UnitSize.y) / 2f;

		if (movePosition.x > -width && movePosition.x < width)
		{
			targetDirection.x = direction.x;
		}
		else
		{
			targetDirection.x = -direction.x;
		}
		if (movePosition.y > -height && movePosition.y < height)
		{
			targetDirection.y = direction.y;
		}
		else
		{
			targetDirection.y = -direction.y;
		}
		targetDirection.Normalize();
		_unit.transform.Translate(targetDirection * deltaTime * 3);

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
