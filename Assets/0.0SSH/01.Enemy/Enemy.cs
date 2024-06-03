using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Agent
{
    public NavMeshAgent _navMeshAgent;
    [SerializeField] private StateMachine<EnemyStateEnum> _stateMachine;
    public EnemyStatus _status;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine.Intialize(this, EnemyStateEnum.Idle);
        
    }

    private void Start()
    {
        EnemyManager.Instance.enemyList.Add(this);
    }
    
    private void OnDestroy()
    {
        EnemyManager.Instance.enemyList.Remove(this);
    }
    

    void Update()
    {
        _stateMachine.Update();
    }

}
