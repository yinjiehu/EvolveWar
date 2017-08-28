using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingAbility : Ability
{
	[SerializeField]
	Sprite[] _clothes;

	public override void Init(BattleUnit unit)
	{
		base.Init(unit);
	}

	public override void OnUpdate(float deltaTime)
	{
		base.OnUpdate(deltaTime);

		var spriteRenderer = _unit.transform.GetComponent<SpriteRenderer>();
		if(spriteRenderer != null)
		{
			var index = _unit.UnitInfo.Level - 1;
			if (index < _clothes.Length)
			{
				spriteRenderer.sprite = _clothes[index];
			}
			else
			{
				throw new System.Exception(string.Format("超出可用图片的最大值：当前等级为{0}，当前衣服数组为{1}", index, _clothes.Length));
			}
		}
		
	}
}
