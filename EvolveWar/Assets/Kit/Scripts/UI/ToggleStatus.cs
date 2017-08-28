using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class ToggleStatus : MonoBehaviour, IPointerClickHandler
{
	public Graphic graphic1;
	public Graphic graphic2;

	[SerializeField]
	bool _isOn;

	public ToggleStatusEvent onValueChanged;

	public void OnPointerClick(PointerEventData eventData)
	{
		IsOn = !IsOn;
		onValueChanged.Invoke(IsOn);
	}

	bool IsOn
	{
		get
		{
			return _isOn;
		}
		set
		{
			_isOn = value;
			graphic1.gameObject.SetActive(_isOn);
			graphic2.gameObject.SetActive(!_isOn);
		}
	}

	public void ShowStatus(bool isOn)
	{
		IsOn = isOn;
	}

	[Serializable]
	public class ToggleStatusEvent : UnityEvent<bool>
	{

	}
}
