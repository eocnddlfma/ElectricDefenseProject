using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BaseEnemyAttack : MonoBehaviour
{
    [SerializeField] protected GameObject AttackEffect;
    [SerializeField] public Agent _agent;
    
    public void Initialize(Agent agent)
    {
        _agent = agent;
    }

    public virtual void Attack(Transform parent, Agent target)
    {
        target.health.DoDamage(_agent.agentStatus.damage);
    }
}
