using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
	public class BloodEffect : MonoBehaviour
	{
		[SerializeField]
		Text _content;

		public void SetText(string content)
		{
			_content.text = content;
		}
	}

}