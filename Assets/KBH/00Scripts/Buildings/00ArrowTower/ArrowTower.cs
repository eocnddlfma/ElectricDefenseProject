using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Building<ArrowTowerStateEnum>
{
   [SerializeField] private GameObject _rangeDebugger;

   public override void ShowDebug()
   {
      _rangeDebugger.SetActive(true);
   }

   public override void HideDebug()
   {
      _rangeDebugger.SetActive(false);
   }

}
