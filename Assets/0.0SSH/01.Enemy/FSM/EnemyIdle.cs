using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        if (!_enemyReference.navMeshAgent.enabled)
        {
            _enemyReference.navMeshAgent.enabled = true;
        }

        state = EnemyStateEnum.Move;
        
        return true;
    }
}
