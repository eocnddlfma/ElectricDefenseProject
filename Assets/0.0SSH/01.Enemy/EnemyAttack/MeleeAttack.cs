using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : BaseEnemyAttack
{
    public override void Attack( Transform parent)
    {
        GameObject g = Instantiate(AttackEffect, parent);
        Destroy(g, 1f);
    }
}
