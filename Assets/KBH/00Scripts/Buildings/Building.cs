using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building<T> : Agent 
   where T : Enum
{
   private BuildingStateMachine<T> mainStateMachine;
   private StateMachine<T> staySubStateMachine;

   private void Awake()
   {
      if(staySubStateMachine is not null)
      {
         mainStateMachine.Intialize(this, staySubStateMachine, BuildingBaseStateEnum.Stay);
      }
   }


}
