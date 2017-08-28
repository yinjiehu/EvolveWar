using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackAbility : Ability
{
	[SerializeField]
	float _attackInterval = 0.2f;

	float _elapsedTime = 0;

	public override void OnUpdate(float deltaTime)
	{
		base.OnUpdate(deltaTime);

		_elapsedTime += Time.deltaTime;
		if(_elapsedTime > _attackInterval)
		{
			var hits = Physics2D.RaycastAll(_unit.transform.position, _unit.MoveDirection.normalized, _unit.UnitInfo.Range);
			foreach (var hit in hits)
			{
				var checkUnit = hit.transform.GetComponent<BattleUnit>();
				if (checkUnit != null && checkUnit.UnitInfo.UnitID != _unit.UnitInfo.UnitID)
				{
					_unit.Attack();
					_elapsedTime = 0;
				}
			}
		}
	}
}
