using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyReference : StatesReference<EnemyStateEnum>
{
    public Enemy _enemy;
    public NavMeshAgent _navMeshAgent;
    public EnemyStatus _status;
    public Animator _animator;
    public BaseEnemyAttack _baseEnemyAttack;

    private void Awake()
    {
        _navMeshAgent = _enemy._navMeshAgent;
        _status = _enemy.enemyStatus;
        _baseEnemyAttack = _enemy._enemyAttack;
    }
}
