using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : PlayerState
{
   public override bool CanChangeToOther(ref GameMode state)
   {
      return base.CanChangeToOther(ref state);
   }
}
