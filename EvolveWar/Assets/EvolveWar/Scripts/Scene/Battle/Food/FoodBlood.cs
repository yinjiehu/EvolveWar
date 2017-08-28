using UnityEngine;
using System.Collections;

public class FoodBlood : Food
{
	public enum FoodBloodEnum
	{
		None,
		Low,
		Middle,
		High
	}

	[SerializeField]
	FoodBloodEnum _bloodEnum;

	public override void Init()
	{
		_exp = (int)_bloodEnum * 10;
	}
}
