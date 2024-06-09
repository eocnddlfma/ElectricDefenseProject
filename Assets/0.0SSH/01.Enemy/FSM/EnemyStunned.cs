using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunned : EnemyState
{
    private float stunTime;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("enemyState stunned");
    }
    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            return false;
        }

        state = EnemyStateEnum.Move;
        return true;
    }
}
