using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public Agent agent;
    public void Initialize(Agent agent)
    {
        this.agent = agent;
    }
    
    public void CastDamage(Agent target)
    {
        target.health.DoDamage(agent.agentStatus.damage);
    }
}
