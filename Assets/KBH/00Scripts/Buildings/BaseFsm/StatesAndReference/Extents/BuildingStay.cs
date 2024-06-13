
using System.Collections.Generic;
using UnityEngine;

class BuildingStay : State<BuildingBaseStateEnum>
{

   public override bool CanChangeToOther(ref BuildingBaseStateEnum state)
   {
      return false;
   }



}
