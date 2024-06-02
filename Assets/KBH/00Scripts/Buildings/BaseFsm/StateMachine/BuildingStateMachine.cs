using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingStateMachine<T> : StateMachine<BuildingBaseStateEnum>
   where T : Enum
{
   [SerializeField] private StateMachine<T> stayStateSubMachine;

   public void Intialize(Agent owner, StateMachine<T> subStateMachine, BuildingBaseStateEnum defaultState)
   {
      base.Intialize(owner, defaultState);
      stayStateSubMachine.Intialize(owner, default);
   }


   public override void Update()
   {
      base.Update();
      if(State == BuildingBaseStateEnum.Stay)
      {
         stayStateSubMachine.Update();
      }
   }
}
