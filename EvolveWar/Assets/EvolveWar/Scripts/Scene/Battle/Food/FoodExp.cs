using UnityEngine;
using System.Collections;

public class FoodExp : Food {

	public enum FoodExpEnum
	{
		None,
		One,
		Two,
		Three,
		Four,
		Five
	}

	[SerializeField]
	FoodExpEnum _expEnum;

	public override void Init()
	{
		_exp = (int)_expEnum * 2;
	}

	public FoodExpEnum ExpEnum
	{
		get
		{
			return _expEnum;
		}
	}
}
