using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : PlayerState
{
   public override bool CanChangeToOther(ref GameMode state)
   {
      if(Input.GetKeyDown(KeyCode.Escape))
      {
         state = GameMode.View;
         return true;
      }
      return false;
   }
}
