using UnityEngine;
using System.Collections;

public interface IAbility
{
    void Init(BattleUnit unit);
    void OnUpdate(float deltaTime);
    void LateUpdate();
}
