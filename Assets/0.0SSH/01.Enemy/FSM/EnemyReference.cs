using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyReference : StatesReference<EnemyStateEnum>
{
    [SerializeField] private Enemy _enemy;
    public NavMeshAgent _navMeshAgent;
    public EnemyStatus _status;
    public Animator _animator;
    public BaseEnemyAttack _baseEnemyAttack;
    public Agent target;

    private void Awake()
    {
        _navMeshAgent = _enemy._navMeshAgent;
        _status = _enemy.status as EnemyStatus;
        _baseEnemyAttack = _enemy._enemyAttack;
    }
}
