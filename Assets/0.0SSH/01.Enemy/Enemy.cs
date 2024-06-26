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
    public EnemyTargetFinder TargetFinder;
    public Animator animatiorComponent;
    public EnemyStatus enemyStatus;


    public Agent target;

    private void Awake()
    {
        enemyStatus = status as EnemyStatus;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        TargetFinder = GetComponent<EnemyTargetFinder>();
        if (GetComponent<MeleeAttack>())
        {
            _enemyAttack = GetComponent<MeleeAttack>() as BaseEnemyAttack;
        }
        if (GetComponent<RangedAttack>())
        {
            _enemyAttack = GetComponent<RangedAttack>() as BaseEnemyAttack;
        }
        //Debug.Log(_enemyAttack);
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

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    void Update()
    {
        _stateMachine.Update();
    }
    
    public bool HasTarget()
    {
        if (target != null && target.gameObject.activeSelf)
        {
            print("has a target");
            return true;
        }
        TargetFinder.FindTarget();
        return false;
    }

}
