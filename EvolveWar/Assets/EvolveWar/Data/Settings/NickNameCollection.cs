﻿using UnityEngine;
using System.Collections;
using Kit.Inspector;
using Kit.Utility.OpenXml;
using System.Collections.Generic;

public class NickNameCollection : MonoBehaviour {

	[SerializeField]
	TextAsset _excel;

	[SerializeField]
	List<string> _nickNames;
#if UNITY_EDITOR
	[SerializeField]
	InspectorButton _loadExcel;

	public void LoadExcel()
	{
		_nickNames = new List<string>();
		var xml = new OpenXmlParser();
		xml.LoadXml(_excel.text);
		var sheet = xml.SelectSheet("NickName");
		while(sheet.MoveNext())
		{
			var nickName = sheet.CurrentRow[0].StringValue;
			if (!string.IsNullOrEmpty(nickName))
			{
				_nickNames.Add(nickName);
			}
		}
	}

#endif

#if !EVOLVEWAR_SERVER

	public string GetRandomName()
	{
		return _nickNames[Random.Range(0, _nickNames.Count)];
	}
#endif
}