using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowTowerStateReference : StatesReference<ArrowTowerStateEnum>
{
   public Enemy closestEnemy;

   public float baseDetectDistance = 2f;
   public int arrowPerSecond = 2;
   public float coolTime => 1 / (float)arrowPerSecond;

   public bool hasBoomSplash = false;
   public float boomSplashRange = 2f;
}
