using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public Agent _agent;
    public void Initialize(Agent agent)
    {
        _agent = agent;
    }
    
    public void CastDamage(Agent target)
    {
        target.health.DoDamage(_agent.status.damage);
    }
}
