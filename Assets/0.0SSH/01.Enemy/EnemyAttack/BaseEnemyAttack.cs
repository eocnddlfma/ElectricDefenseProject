using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyAttack : MonoBehaviour
{
    [SerializeField] protected GameObject AttackEffect;
    public abstract void Attack(Transform parent);
}
