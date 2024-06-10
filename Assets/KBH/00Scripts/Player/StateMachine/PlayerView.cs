using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : PlayerState
{
   public override bool CanChangeToOther(ref GameMode state)
   {
      bool isClick = InputUtil.isClick;
      bool isSelected = Shot3DUtil.GetAgentOnCurrentCursor();


      if (isClick && isSelected)
      {
         state = GameMode.Upgrade;
         return true;
      }

      return base.CanChangeToOther(ref state);
   }
}
