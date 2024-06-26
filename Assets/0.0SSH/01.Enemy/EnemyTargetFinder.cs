using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTargetFinder : MonoBehaviour
{
    private Enemy enemy;
    public LayerMask resourceBuildingLayer;
    public LayerMask wallBuildingLayer;
    public LayerMask attackBuildingLayer;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public Transform closest;
    private RaycastHit[] _raycastHits = new RaycastHit[99];
    [SerializeField]public List<Transform> fordebug;
    public void FindTarget()
    {
        if (enemy.enemyStatus.moveTargetPriority  == EnemyTargetPriorityEnum.CommandHousePrior) // 중앙 타워 우선일 경우
        {
            var hits = Physics.SphereCastNonAlloc(
                transform.position, enemy.enemyStatus.attackRadius/2,//보이는 곳에 벽 빌딩이 있는가?
                Vector3.up, _raycastHits, 0f, wallBuildingLayer);//layer8 == wallbuilding
            print(hits);
            if (hits > 0)
            {
                foreach(var a in _raycastHits)
                {
                    print(a.transform.name);
                    a.transform.GetComponent<HighWall>().Die();//벽 그대로 부숨
                    a.transform.GetComponent<LowWall>().Die();//벽 그대로 부숨
                }
                return;
            }
        }
        if (enemy.enemyStatus.moveTargetPriority == EnemyTargetPriorityEnum.ResourcePrior) // 자원도둑일 경우
        {
            var hits = Physics.SphereCastNonAlloc(
                transform.position, enemy.enemyStatus.attackRadius*7,//보이는 곳에 벽 빌딩이 있는가?
                Vector3.up, _raycastHits, 0f, resourceBuildingLayer);//layer7 == resourcebuilding

            if (hits > 0)
            {
                enemy.target = _raycastHits[0].transform.GetComponent<Agent>();//해당 자원 빌딩을 우선으로 이동
                return;
            }
        }
        
        // 아무 빌딩 확인일 경우
        {
            //if(BuildingUtil.Instance.buildingList.Count>0) //아무 빌딩이라도 존재할 경우
            
            //    enemy.target = GetClosestBuilding(BuildingUtil.Instance.buildingList.ToArray());//위치가 가장 가까운 빌딩 선택
            var hits = Physics.SphereCastNonAlloc(
                transform.position, enemy.enemyStatus.attackRadius*10,//보이는 곳에 벽 빌딩이 있는가?
                Vector3.up, _raycastHits, 0f, resourceBuildingLayer | wallBuildingLayer | attackBuildingLayer);//layer 모든 빌딩
            print(hits);
            if (hits > 0)
            { 
                fordebug.Clear();
                foreach (var a in _raycastHits)
                {
                    if(a.transform != null)
                    fordebug.Add(a.transform);
                }
                Vector3 myPos = transform.position;
                closest = transform;
                Debug.Log("agent : " + closest);

                if (fordebug.Count < 1)
                    return;
                fordebug.Add(transform);
                do
                {
                    fordebug.Remove(closest);
                    closest = fordebug[0];
                    for (int i = 1; i < fordebug.Count; i++)
                    {
                        if (!fordebug[i].gameObject.activeSelf)
                        {
                            fordebug.Remove(fordebug[i]);
                            i--;
                            continue;
                        }
                        if (Vector3.Distance(myPos, fordebug[i].position) < Vector3.Distance(myPos, closest.position))
                        {
                            closest = fordebug[i];
                        }
                    }
                    enemy.target = closest.GetComponent<Agent>();
                } while (!EnemyRouteManager.Instance.HasRoute(enemy._navMeshAgent));
                Debug.Log(closest + "closest");
                print("foundTarget : " + enemy.target.name);
            }
        }
    }    
    

    private void Update()
    {
        OnDrawGizmos();
    }

    private void OnDrawGizmos()
    {
//        Gizmos.DrawWireSphere(transform.position, enemy.enemyStatus.attackRadius*5);
    }
}
