using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State<EnemyStateEnum>
{
    public EnemyReference _enemyReference => _reference as EnemyReference;
    
}
