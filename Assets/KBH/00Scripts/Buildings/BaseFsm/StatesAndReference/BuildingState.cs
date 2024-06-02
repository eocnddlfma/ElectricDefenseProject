using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : State<BuildingBaseStateEnum>
{
   public override bool CanChangeToOther(ref BuildingBaseStateEnum state)
   {
      return false;
   }
}
