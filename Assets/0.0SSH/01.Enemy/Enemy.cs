using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Agent
{
    public NavMeshAgent _navMeshAgent;
    private StateMachine<EnemyStateEnum> _stateMachine;
    public EnemyStatus _status;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    void Update()
    {
        
    }

}
