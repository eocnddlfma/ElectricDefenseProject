using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State<GameMode>
{
   protected PlayerStatesReference Reference => _reference as PlayerStatesReference;

   public override bool CanChangeToOther(ref GameMode state)
   {
      if(state != Reference.currentGameMode)
      {
         state = Reference.currentGameMode;
         return true;
      }
      return false;
   }
}
