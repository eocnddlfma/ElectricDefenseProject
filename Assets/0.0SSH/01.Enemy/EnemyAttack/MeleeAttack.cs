using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : BaseEnemyAttack
{
    public override void Attack(Transform parent, Agent target)
    {
        Debug.Log("meleeattack");
        base.Attack(parent, target);
    }
}
