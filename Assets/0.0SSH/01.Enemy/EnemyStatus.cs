using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyStatus : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public EnemyTargetPriorityEnum moveTargetPriority;

    public float attackRadius;
}