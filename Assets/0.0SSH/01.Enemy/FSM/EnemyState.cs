using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State<EnemyStateEnum>
{
    public EnemyReference _enemyReference => _reference as EnemyReference;
    public string animationString;
    private int animationHash;

    private void Awake()
    {
        animationHash = Animator.StringToHash(animationString);
    }

    public override void Enter()
    {
        base.Enter();
        _enemyReference._animator.SetBool(animationHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        _enemyReference._animator.SetBool(animationHash, false);
    }
}
