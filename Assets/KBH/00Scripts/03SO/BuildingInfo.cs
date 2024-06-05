using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BuildingInfo")]
public class BuildingInfo : ScriptableObject
{
   public int hp;
   public int range;
   public int cost;
   public BuildingUseType useType;
   public Color baseColor;
   public string buildingName;
   [TextArea(2,5)] public string description;

   public static int GetStatusByLevel(int level, int startValue, int increasePerLevel) 
   {
      return startValue + level*level * increasePerLevel/3;
   }

   public static int GetCostByLevel(int level, int startCost, int increasePerLevel)
   {
      return startCost + level * level * level * increasePerLevel / 5;
   }

}
