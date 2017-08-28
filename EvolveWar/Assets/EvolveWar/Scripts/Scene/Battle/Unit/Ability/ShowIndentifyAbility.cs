using UnityEngine;
using System.Collections;
using System;
using Kit.UI;
using UI.Battle;

public class ShowIndentifyAbility : Ability
{
	Transform _hpPos;

    public override void Init(BattleUnit unit)
    {
        base.Init(unit);
		_hpPos = _unit.transform.FindChild("HpPos");
		MainUI.Instance.GetPanel<BattlePanel>().GenerateHpBar(_hpPos, _unit.UnitInfo);
	}

    public override void OnUpdate(float deltaTime)
    {
    }

}
