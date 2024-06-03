using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMove : EnemyState
{
    public Agent TargetBuilding;
    
    public override void Enter()
    {
        base.Enter();
        Debug.Log("enemyStateMove");
        EnemyRouteManager.Instance.OnMapChange.AddListener(CheckRouteWhenMapChanged);
        _enemyReference._navMeshAgent.enabled = true;
    }

    private RaycastHit[] _raycastHits;
    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        
        if (_enemyReference._enemyStatus.moveTargetPriority == EnemyTargetPriorityEnum.CommandHousePrior)
        {
            if (EnemyRouteManager.Instance.HasRouteToBuilding(_enemyReference._navMeshAgent))
            {
                TargetBuilding = EnemyRouteManager.Instance.CommandBuilding;
            }
        }
        else if (_enemyReference._enemyStatus.moveTargetPriority == EnemyTargetPriorityEnum.ResourcePrior)
        {
            var hits = Physics.SphereCastNonAlloc(
                transform.position, _enemyReference._enemyStatus.attackRadius,//보이는 곳에 자원 빌딩이 있는가?
                Vector3.up, _raycastHits, 0f, 7);//layer7 == resourcebuilding

            if (hits > 0)
            {
                TargetBuilding = _raycastHits[0].transform.GetComponent<Agent>();
            }
            
            
        }
        else
        {
            //TargetBuilding = GetClosestBuilding();
        }

        TargetBuilding = EnemyRouteManager.Instance.CommandBuilding;//forTest;
        _enemyReference._navMeshAgent.SetDestination(TargetBuilding.transform.position);
        
        if(EnemyRouteManager.Instance.HasRouteToBuilding(_enemyReference._navMeshAgent))
        {
            //Debug.LogError("야임마 길 없어!!");
        }
        if (Vector3.Distance(TargetBuilding.transform.position, transform.position) <
            _enemyReference._enemyStatus.attackRadius)
        {
            state = EnemyStateEnum.Attack;
            return true;
        }
        
        return false;
    }
    
    public void CheckRouteWhenMapChanged()
    {
        if(EnemyRouteManager.Instance.HasRouteToBuilding(_enemyReference._navMeshAgent))
        {
            TargetBuilding = null;
            Debug.LogError("야임마 길 없어!!");
        }
    }

    public override void Exit()
    {
        base.Exit();
        _enemyReference._navMeshAgent.enabled = false;
        EnemyRouteManager.Instance.OnMapChange.RemoveListener(CheckRouteWhenMapChanged);
    }

    public Agent GetClosestBuilding(Agent[] buildingList)
    {
        Agent closest = buildingList[0];
        Vector3 myPos = transform.position;
        for (int i = 1; i < buildingList.Length; i++)
        {
            if (Vector3.Distance(myPos, buildingList[i].transform.position)<Vector3.Distance(myPos, closest.transform.position))
            {
                closest = buildingList[i];
            }
        }

        return closest;
    }

    private void OnDrawGizmos()
    {
        
        //Gizmos.DrawWireSphere(transform.position, _enemyReference._enemyStatus.attackRadius);
        
    }
}
