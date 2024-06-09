using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : BaseEnemyAttack
{
    public void Attack(Transform parent)
    {
        Instantiate(AttackEffect, parent);
    }
    
    
}
