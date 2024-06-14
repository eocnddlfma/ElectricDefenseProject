using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTowerStay : State<ArrowTowerStateEnum>
{
   public float attackDetectRadius = 2f;
   public ArrowTowerStateReference Reference
   => _reference as ArrowTowerStateReference;

   public override bool CanChangeToOther(ref ArrowTowerStateEnum state)
   {
      List<Enemy> enemyList = EnemyManager.Instance.enemyList;

      for (int i = 0; i < enemyList.Count; ++i)
      {
         if (enemyList[i] is not null)
         {
            float betweenDistance
                 = Vector3.Distance(enemyList[i].transform.position, transform.position);

            if (betweenDistance < Reference.baseDetectDistance * attackDetectRadius)
            {
               state = ArrowTowerStateEnum.Attack;
               return true;
            }
         }
      }

      return false;
   }

#if UNITY_EDITOR
   public bool isGizmoDraw = false;
   private void OnDrawGizmos()
   {
      if (!isGizmoDraw) return;
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, attackDetectRadius);
      Gizmos.color = Color.white;
   }
#endif

}
