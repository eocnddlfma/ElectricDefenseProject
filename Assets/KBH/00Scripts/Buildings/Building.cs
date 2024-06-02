using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building<T> : Agent 
   where T : Enum
{
   [SerializeField] private BuildingStateMachine<T> mainStateMachine;

   public virtual void Awake()
   {
      mainStateMachine.Intialize(this, BuildingBaseStateEnum.Stay);
   }


}
