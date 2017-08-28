using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderFillter : MonoBehaviour {

	UnitInfo _unitInfo;

	public void Init(UnitInfo info)
	{
		_unitInfo = info;
        GetComponent<PolygonCollider2D>().isTrigger = true;
        Debug.Log("Init AttackCollider name is " + _unitInfo.Unit.name);
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		//Debug.Log("attack collider name :" + collider2D.transform.name);
		var beAttacked = collider2D.transform.GetComponent<BattleUnit>();

		if(_unitInfo == null)
		{
			throw new System.Exception("不能没有攻击者" + beAttacked.name);
		}
		if (beAttacked != null)
		{
			if(beAttacked.UnitInfo.UnitID != _unitInfo.UnitID)
			{
				beAttacked.BeAttaked(_unitInfo.Unit);
			}
		}
	}
}
