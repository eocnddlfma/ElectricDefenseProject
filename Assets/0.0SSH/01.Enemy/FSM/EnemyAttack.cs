using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    public override bool CanChangeToOther(ref EnemyStateEnum state)
    {
        
        return false;
    }
}
