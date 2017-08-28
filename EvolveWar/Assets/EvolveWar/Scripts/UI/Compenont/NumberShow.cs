using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NumberShow : MonoBehaviour
{
	[SerializeField]
	List<Sprite> _numberSprites;

	[SerializeField]
	Image _icon;


	public void UpdateData(int num)
	{
		_icon.sprite = _numberSprites[num];
	}
}
