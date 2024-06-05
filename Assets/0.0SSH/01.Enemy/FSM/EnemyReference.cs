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
    public EnemyStatus _enemyStatus;
    public Animator _animator;
    public BaseEnemyAttack _baseEnemyAttack;
    public Agent target;

    private void Awake()
    {
        _navMeshAgent = _enemy._navMeshAgent;
        _enemyStatus = _enemy._status;
        _baseEnemyAttack = _enemy._enemyAttack;
    }
}
