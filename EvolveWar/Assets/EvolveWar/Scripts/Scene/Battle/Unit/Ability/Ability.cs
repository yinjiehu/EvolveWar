using UnityEngine;
using System.Collections;
using System;

public class Ability : MonoBehaviour, IAbility
{
    protected BattleUnit _unit;

    public virtual void Init(BattleUnit unit)
    {
        this._unit = unit;
    }


    public virtual void OnUpdate(float deltaTime)
    {
    }

    public virtual void LateUpdate()
    {
    }

}
