using UnityEngine;
using System.Collections;

public class CollisionFilterAbility : Ability
{
	public override void Init(BattleUnit unit)
	{
		base.Init(unit);
	}

	protected void OnTriggerEnter2D(Collider2D collider)
	{
		var food = collider.gameObject.GetComponent<Food>();
		if(food != null)
		{
			//_unit.AddExp(food.Exp);
			_unit.EatFood(food);
			BattleScene.Instance.FoodMgr.RemoveFood(food);
		}
		//Debug.Log("collider name is " + collider.gameObject.name);
	}
}
