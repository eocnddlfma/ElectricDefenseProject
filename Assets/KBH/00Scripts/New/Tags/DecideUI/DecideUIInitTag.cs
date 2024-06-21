using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecideUIInitTag : MonoTag<bool>
{
   [SerializeField] private DecideUIReference _uiReference;
   private bool isFirstUpdate = true;


   public override void Initialize()
   {

   }

   public void OnEnter()
   {
      int idx = 0;
      int maxIdx = _uiReference.currentOpenedBlock.childsContainsDummy.Count-1;

      foreach (var block
         in _uiReference.currentOpenedBlock.childsContainsDummy)
      {
         Tween tween = block.SetVisible(true, idx);
         if (idx == maxIdx)
         {
            tween.OnComplete(() =>
            {
               Debug.Log("ÇÏ±ä ÇÔ ;;");
               Exit();
            });
         }

         ++idx;
      }

      foreach (var block in _uiReference.currentOpenedBlock)
      {
         if (block is EventUIBlock)
         {
            EventUIBlock eventBlock = block as EventUIBlock;

            if (eventBlock.executeType == EventExecuteCondition.OnEnter)
            {
               eventBlock.executeCallback?.Invoke();
            }
         }
      }

   }

   public override void BaseUpdate()
   {
      if (isFirstUpdate)
      {
         OnEnter();
         isFirstUpdate = false;
      }
   }

   public void Exit()
   {
      isFirstUpdate = true;
      Current = false;
   }

}
