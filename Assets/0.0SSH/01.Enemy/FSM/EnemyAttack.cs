using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    private bool isAttackFinished;
    private int animationLength; //fps 60이라 가정
    public override void Enter()
    {
        base.Enter();
        Debug.Log("enemyStateAttack");
        //에너미 공격 애니메이션 실행
        StartCoroutine(Attack());
        isAttackFinished = false;
        //animationLength = _enemyReference._animator.GetCurrentAnimatorClipInfo(0).Length;
        //_enemyReference._animator.speed = _enemyReference._enemyStatus.attackTimeMultiplier;
    }

    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        if(!isAttackFinished)
            return false;

        state = EnemyStateEnum.Move;
        return true;
    }

    private IEnumerator Attack()
    {
        Debug.Log("attacking");
        _enemyReference._baseEnemyAttack.Attack(transform);
        //yield return new WaitForSeconds(animationLength / _enemyReference._enemyStatus.attackTimeMultiplier);
        yield return new WaitForSeconds(1f);
        isAttackFinished = true;
    }
}
