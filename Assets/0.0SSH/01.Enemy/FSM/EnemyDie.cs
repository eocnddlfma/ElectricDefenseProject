using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("enemyState die");
    }
    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        return false;
    }
}
