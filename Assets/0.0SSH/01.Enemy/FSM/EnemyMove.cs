using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemyState
{
    public Agent TargetBuilding;
    
    public override void Enter()
    {
        base.Enter();
        EnemyRouteManager.Instance.OnMapChange.AddListener(CheckRouteWhenMapChanged);
        if (EnemyRouteManager.Instance.HasRouteToBuilding(_enemyReference.navMeshAgent))
        {
            
        }
    }

    private RaycastHit[] _raycastHits;
    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        if(Vector3.Distance(TargetBuilding.transform.position, transform.position) < _enemyReference.enemyStatus.attackRadius)
        
        if (_enemyReference.enemyStatus.moveTargetPriority == EnemyTargetPriorityEnum.CommandHousePrior)
        {
            if (EnemyRouteManager.Instance.HasRouteToBuilding(_enemyReference.navMeshAgent))
            {
                TargetBuilding = EnemyRouteManager.Instance.CommandBuilding;
            }
        }
        else if (_enemyReference.enemyStatus.moveTargetPriority == EnemyTargetPriorityEnum.ResourcePrior)
        {
            var hits = Physics.SphereCastNonAlloc(
                transform.position, _enemyReference.enemyStatus.attackRadius,//보이는 곳에 자원 빌딩이 있는가?
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
        _enemyReference.navMeshAgent.SetDestination(TargetBuilding.transform.position);
        
        if(EnemyRouteManager.Instance.HasRouteToBuilding(_enemyReference.navMeshAgent))
        {
            TargetBuilding = null;
            Debug.LogError("야임마 길 없어!!");
        }
        
        return false;
    }
    
    public void CheckRouteWhenMapChanged()
    {
        if(EnemyRouteManager.Instance.HasRouteToBuilding(_enemyReference.navMeshAgent))
        {
            TargetBuilding = null;
            Debug.LogError("야임마 길 없어!!");
        }
    }

    public override void Exit()
    {
        base.Exit();
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
}
