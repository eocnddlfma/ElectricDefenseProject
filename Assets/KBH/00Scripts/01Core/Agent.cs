using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
   public AgentType agentType;
   public AgentStatus agentStatus;
   public Health health;
   public virtual void WakeUpAction()
   {

   }

   public virtual void Awake()
   {
      Debug.Log("awa");
      health.Initialize(this);
   }

   public void Die()
   {
      
   }
}
