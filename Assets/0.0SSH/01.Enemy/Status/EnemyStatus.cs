using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyStatus : AgentStatus
{
    public float attackTimeMultiplier;

    public EnemyTargetPriorityEnum moveTargetPriority;

    public float attackRadius;
}
