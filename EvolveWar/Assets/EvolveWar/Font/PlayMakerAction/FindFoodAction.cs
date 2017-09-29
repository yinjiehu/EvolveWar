using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EvolveWar.Actions
{
	public class FindFoodAction : FsmStateAlibity
	{
		Vector3 _targetPosition;

		FollowCamera camera;

		public override void OnEnter()
		{
			base.OnEnter();
			var food = BattleScene.Instance.FoodMgr.GetRandomFood();
			_targetPosition = food.transform.position;
			_unit.SetMoveDierection(_targetPosition - _unit.transform.position);
			
			camera = Object.FindObjectOfType<FollowCamera>();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			if (CheckTarget())
			{
				_unit.Fsm.SendEvent("AutoAttack");
			}else
			{
				if (Vector3.Distance(_unit.transform.position, _targetPosition) < 0.5f || !camera.IsContiansPoint(_unit.transform.position))
				{
					var food = BattleScene.Instance.FoodMgr.GetRandomFood();
					_targetPosition = food.transform.position;
					_unit.SetMoveDierection(_targetPosition - _unit.transform.position);
				}
			}
		}

		bool CheckTarget()
		{
			var currentPoint = new Vector2(_unit.transform.position.x, _unit.transform.position.y);
			var colliders = Physics2D.OverlapCircleAll(currentPoint, 3f);

			for (int i = 0; i < colliders.Length; i++)
			{
				var current = colliders[i];

				var currentRangeUnit = current.transform.GetComponent<BattleUnit>();
				if (currentRangeUnit != null && currentRangeUnit.UnitInfo.Uid != _unit.UnitInfo.Uid)
				{
					return true;
				}
			}
			return false;
		}
	}
}