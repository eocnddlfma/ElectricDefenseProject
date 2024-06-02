using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTowerAttack : State<ArrowTowerStateEnum>
{
   public override bool CanChangeToOther(ref ArrowTowerStateEnum state)
   {
      return false;
   }
}
