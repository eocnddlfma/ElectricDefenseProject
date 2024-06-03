using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : Agent
{
    public NavMeshAgent _navMeshAgent;
    [SerializeField] private StateMachine<EnemyStateEnum> _stateMachine;
    public EnemyStatus _status;
    public BaseEnemyAttack _enemyAttack;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (GetComponent<MeleeAttack>())
        {
            _enemyAttack = GetComponent<MeleeAttack>() as BaseEnemyAttack;
        }
        Debug.Log(_enemyAttack);
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
