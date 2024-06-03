using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnemyRouteManager : MonoSingleton<EnemyRouteManager>
{
    //게임 맵 중앙에 있는 커멘드 빌딩
    [SerializeField] public Agent CommandBuilding;

    public UnityEvent OnMapChange; 
    
    void Start()
    {
        //CommandBuilding = GameObject.Find("CommandBuilding");
    }

    public bool HasRouteToBuilding(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.hasPath)
        {
            return true;
        }
        return false;
    }

    public void MapChanged()
    {
        OnMapChange?.Invoke();
    }

    
    public Transform GetBuildingPosition()
    {
        return CommandBuilding.transform;
    }
}
