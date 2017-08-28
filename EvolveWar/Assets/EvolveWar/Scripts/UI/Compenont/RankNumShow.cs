using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankNumShow : MonoBehaviour
{
	[SerializeField]
	NumberShow _shiNum;
	[SerializeField]
	NumberShow _geNum;
	[SerializeField]
	NumberShow _specialNum;

	public void UpdateData(int num)
	{
		if(num <= 3)
		{
			_specialNum.gameObject.SetActive(true);
			_shiNum.gameObject.SetActive(false);
			_geNum.gameObject.SetActive(false);
			_specialNum.UpdateData(num);
		}else
		{

			_specialNum.gameObject.SetActive(false);

			if(num >= 10)
			{
				_shiNum.gameObject.SetActive(true);
				_geNum.gameObject.SetActive(true);
			}
			else
			{
				_shiNum.gameObject.SetActive(false);
				_geNum.gameObject.SetActive(true);
			}

			int shi = Mathf.FloorToInt(num / 10f);
			int ge = num % 10;
			_shiNum.UpdateData(shi);
			_geNum.UpdateData(ge);
		}
	}
}
