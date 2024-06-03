using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyStatus : MonoBehaviour
{
    public float hp;
    public float maxHp;
    [FormerlySerializedAs("attackTime")] public float attackTimeMultiplier;

    public EnemyTargetPriorityEnum moveTargetPriority;

    public float attackRadius;
}
