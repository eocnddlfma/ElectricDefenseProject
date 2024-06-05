using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
   public AgentType agentType;
   public AgentStatus agentStatus;
   public Health health;
   public DamageCaster damageCaster;
   [field: SerializeField] public Vector2Int cellPosition { get; set; }
   public virtual void WakeUpAction()
   {

   }
}
