using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
   public AgentType agentType;
   public AgentStatus agentStatus;
   public Health health;
   public DamageCaster damageCaster;
   public virtual void WakeUpAction()
   {

   }
}
