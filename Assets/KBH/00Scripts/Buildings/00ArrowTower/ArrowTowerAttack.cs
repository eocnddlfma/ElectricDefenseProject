using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTowerAttack : State<ArrowTowerStateEnum>
{
   public float attackUnwindDetectRadius = 2f;

   public override bool CanChangeToOther(ref ArrowTowerStateEnum state)
   {
      List<Enemy> enemyList = EnemyManager.Instance.enemyList;
      for (int i = 0; i<enemyList.Count; ++i)
      {
         if (enemyList[i])
         {
            float betweenDistance
               = Vector3.Distance(enemyList[i].transform.position, transform.position);

            if(betweenDistance < attackUnwindDetectRadius)
            {
               break;
            }
            else if(i == enemyList.Count - 1)
            {
               state = ArrowTowerStateEnum.Stay;
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
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, attackUnwindDetectRadius);
      Gizmos.color = Color.white;
   }
#endif
}
