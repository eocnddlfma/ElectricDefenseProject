using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReference : StatesReference<EnemyStateEnum>
{
    [SerializeField] private Enemy enemy;
    public NavMeshAgent navMeshAgent;
    public EnemyStatus enemyStatus;

    private void Awake()
    {
        navMeshAgent = enemy._navMeshAgent;
        enemyStatus = enemy._status;
    }
}
