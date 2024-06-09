using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : BaseEnemyAttack
{
    public override void Attack(Transform parent, Agent target)
    {
        StartCoroutine(Doattack(1.5f, parent, target));
    }
    public override void Attack(Transform parent, Agent target, float time)
    {
        StartCoroutine(Doattack(time - 0.2f, parent, target));
    }

    public IEnumerator Doattack(float waitSec, Transform parent, Agent target)
    {
        yield return new WaitForSeconds(waitSec);
        ActualAttack(parent, target);
    }

    public void ActualAttack(Transform parent, Agent target)
    {
        print("asdf");
        GameObject g = Instantiate(AttackEffect, parent);
        g.GetComponent<EnemyProjectile>().Setting(transform.position, target.transform.position);
        base.Attack(parent, target, 0.5f);
        
    }
    
}
