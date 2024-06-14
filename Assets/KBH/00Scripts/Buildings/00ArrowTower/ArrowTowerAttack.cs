using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTowerAttack : State<ArrowTowerStateEnum>
{
   public float attackUnwindDetectRadius = 2f;
   public ArrowTowerStateReference Reference
      => _reference as ArrowTowerStateReference;

   public override bool CanChangeToOther(ref ArrowTowerStateEnum state)
   {
      List<Enemy> enemyList = EnemyManager.Instance.enemyList;

      float minDistance = float.MaxValue;
      Enemy closestEnemy = null;

      bool isAllEnemyOutOfRadius = true;

      for (int i = 0; i<enemyList.Count; ++i)
      {
         if (enemyList[i])
         {
            float betweenDistance
               = Vector3.Distance(enemyList[i].transform.position, transform.position);

            if(betweenDistance < Reference.baseDetectDistance * attackUnwindDetectRadius)
            {
               isAllEnemyOutOfRadius = false;

               if (betweenDistance < minDistance)
               {
                  betweenDistance = minDistance;
                  closestEnemy = enemyList[i];
               }
            }
            else if(i == enemyList.Count - 1 && isAllEnemyOutOfRadius)
            {
               state = ArrowTowerStateEnum.Stay;
               return true;
            }

            
         }
      }

      Reference.closestEnemy = closestEnemy;
      return false;
   }

#if UNITY_EDITOR
   public bool isGizmoDraw = false;
   private void OnDrawGizmos()
   {
      if (!isGizmoDraw) return;
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, attackUnwindDetectRadius);
      Gizmos.color = Color.white;
   }
#endif
}
