using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLineCarry : State<EnergyLineStateEnum>
{
   public override bool CanChangeToOther(ref EnergyLineStateEnum state)
   {
      return false;
   }
}
