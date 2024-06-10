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
    public BaseEnemyAttack _enemyAttack;
    public DamageCaster _damageCaster;
    public Animator animatiorComponent;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (GetComponent<MeleeAttack>())
        {
            _enemyAttack = GetComponent<MeleeAttack>() as BaseEnemyAttack;
        }
        if (GetComponent<RangedAttack>())
        {
            _enemyAttack = GetComponent<RangedAttack>() as BaseEnemyAttack;
        }
        Debug.Log(_enemyAttack);
        _stateMachine.Intialize(this, EnemyStateEnum.Idle);
        
        _enemyAttack.Initialize(this);
        _damageCaster.Initialize(this);
        base.Awake();
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
