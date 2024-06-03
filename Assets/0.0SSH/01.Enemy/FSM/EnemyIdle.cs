using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    
    public override void Enter()
    {
        base.Enter();
        Debug.Log("enemyStateIdle");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        if (!_enemyReference._navMeshAgent.enabled)
        {
            _enemyReference._navMeshAgent.enabled = true;
        }

        state = EnemyStateEnum.Move;
        
        return true;
    }
}
