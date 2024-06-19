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
    public Agent target; 
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    
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
            }

            if (EnemyRouteManager.Instance.HasRoute(enemy._navMeshAgent))
            {
                enemy.target = EnemyRouteManager.Instance.CommandBuilding;
            }
            else
            {
                hits = Physics.SphereCastNonAlloc(
                    transform.position, enemy.enemyStatus.attackRadius*5,//보이는 곳에 벽 빌딩이 있는가?
                    Vector3.up, _raycastHits, 0f, resourceBuildingLayer |wallBuildingLayer);//layer 모든 빌딩

                if (hits > 0)
                {
                    enemy.target = _raycastHits[0].transform.GetComponent<Agent>();//아무빌딩이나 시야에 잡히면 그 빌딩 부시러 감 ㅂ
                }
                else
                {
                    hits = Physics.SphereCastNonAlloc(
                        transform.position, enemy.enemyStatus.attackRadius*7,//보이는 곳에 벽 빌딩이 있는가?
                        Vector3.up, _raycastHits, 0f, resourceBuildingLayer | wallBuildingLayer | attackBuildingLayer);//layer 모든 빌딩

                    if (hits > 0)
                    {
                        enemy.target = _raycastHits[0].transform.GetComponent<Agent>();//아무빌딩이나 시야에 잡히면 그 빌딩 부시러 감 ㅂ
                    }
                }
            }
        }
        else if (enemy.enemyStatus.moveTargetPriority == EnemyTargetPriorityEnum.ResourcePrior) // 자원도둑일 경우
        {
            var hits = Physics.SphereCastNonAlloc(
                transform.position, enemy.enemyStatus.attackRadius*7,//보이는 곳에 벽 빌딩이 있는가?
                Vector3.up, _raycastHits, 0f, resourceBuildingLayer);//layer7 == resourcebuilding

            if (hits > 0)
            {
                enemy.target = _raycastHits[0].transform.GetComponent<Agent>();//해당 자원 빌딩을 우선으로 이동
            }
        }
        else // 아무 빌딩 확인일 경우
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
                    fordebug.Add(a.transform);
                }
                Vector3 myPos = transform.position;
                Agent closest = fordebug[0].GetComponent<Agent>();
                Debug.Log("agent : " + closest);
                float distance = Vector3.Distance(myPos, closest.transform.position);
                
                for (int i = 1; i < fordebug.Count; i++)
                {
                    if (fordebug[i]) break;
                    if (Vector3.Distance(myPos, fordebug[i].position) < distance)
                    {
                        closest = fordebug[i].GetComponent<Agent>();
                        distance = Vector3.Distance(myPos, closest.transform.position);
                    }
                }
                
                Debug.Log(closest + "closest");
                target = closest;
                enemy.target = closest;
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
