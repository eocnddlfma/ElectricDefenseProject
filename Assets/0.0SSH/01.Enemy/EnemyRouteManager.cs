using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnemyRouteManager : MonoSingleton<EnemyRouteManager>
{
    //게임 맵 중앙에 있는 커멘드 빌딩
    [SerializeField] public Agent CommandBuilding;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private NavMeshData _navMeshData;
    public UnityEvent OnMapChange; 
    
    void Start()
    {
        //CommandBuilding = GameObject.Find("CommandBuilding");
        MapChanged();
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
        _navMeshSurface.UpdateNavMesh(_navMeshData);
        OnMapChange?.Invoke();
    }

    
    public Transform GetBuildingPosition()
    {
        return CommandBuilding.transform;
    }
}
