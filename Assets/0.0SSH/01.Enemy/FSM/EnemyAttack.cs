using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    private bool isAttackFinished;
    private float animationLength; //fps 30이라 가정
    public override void Enter()
    {
        base.Enter();
        Debug.Log("enemyStateAttack");
        //에너미 공격 애니메이션 실행
        StartCoroutine(Attack());
        isAttackFinished = false;
        //_enemyReference._animator.speed = _enemyReference._enemyStatus.attackTimeMultiplier;

        
    }
    

    private IEnumerator Attack()
    {
        yield return null;
        yield return null;
        yield return null;
        animationLength = _enemyReference._animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        print(_enemyReference._animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        _enemyReference._baseEnemyAttack.Attack(transform, _enemyReference._enemy.target, animationLength);
        //yield return new WaitForSeconds(animationLength / _enemyReference._enemyStatus.attackTimeMultiplier);
        yield return new WaitForSeconds(animationLength*0.5f);
        isAttackFinished = true;
    }

    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        if(!isAttackFinished)
            return false;

        state = EnemyStateEnum.Move;
        return true;
    }
}

