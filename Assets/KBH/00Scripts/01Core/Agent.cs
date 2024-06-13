using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
   public AgentType agentType;
   public AgentStatus status;
   public Health health;
   public DamageCaster damageCaster;
   public CapsuleCollider collider;
   [field: SerializeField] public Vector2Int cellPosition { get; set; }
   public virtual void WakeUpAction()
   {

   }

   public virtual void Awake()
   {
      Debug.Log(this);
      collider = GetComponent<CapsuleCollider>();
      health.Initialize(this);
   }

}
