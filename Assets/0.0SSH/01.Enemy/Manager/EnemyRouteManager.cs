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
    [SerializeField] private NavMeshSurface[] _navMeshSurface;
    [SerializeField] private NavMeshData[] _navMeshData;
    
    void Start()
    {
        //CommandBuilding = GameObject.Find("CommandBuilding");
        NavMeshUpdate();
        StartCoroutine(UpdateMap());
    }

    public IEnumerator UpdateMap()
    {
        yield return new WaitForSeconds(2f);
        NavMeshUpdate();
    }
    
    public bool HasRoute(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.hasPath)
        {
            return true;
        }
        return false;
    }

    public void NavMeshUpdate()
    {
        _navMeshSurface[0].UpdateNavMesh(_navMeshData[0]);
        _navMeshSurface[1].UpdateNavMesh(_navMeshData[1]);
        print("navmeshupdated");
    }
}
