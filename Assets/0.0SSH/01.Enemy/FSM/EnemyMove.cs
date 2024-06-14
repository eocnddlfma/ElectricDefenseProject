using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMove : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("enemyStateMove");
        _enemyReference._navMeshAgent.enabled = true;
    }

    private RaycastHit[] _raycastHits;
    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        if (_enemyReference.HasTarget())
        {
            _enemyReference._navMeshAgent.SetDestination(_enemyReference.refTarget.transform.position);
            print(_enemyReference.refTarget);
            if (Vector3.Distance(_enemyReference.refTarget.transform.position, transform.position) <
                _enemyReference._status.attackRadius)
            {
                state = EnemyStateEnum.Attack;
                return true;
            }
        }
        else
        {
            bool isset = _enemyReference._navMeshAgent.SetDestination(new Vector3(0, 0, 0));
        }
        if(EnemyRouteManager.Instance.HasRouteToBuilding(_enemyReference._navMeshAgent))
        {
            Debug.Log("야임마 길 없어!!");
        }
        
        
        return false;
    }

    public override void Exit()
    {
        base.Exit();
        _enemyReference._navMeshAgent.enabled = false;
    }
}
