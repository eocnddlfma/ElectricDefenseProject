using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecideUIExitTag : MonoTag<bool>
{
   // 여기서는 표시되어 있던 UI를 지워야 한다. 
   [SerializeField] private DecideUIReference _uiReference;
   private bool isFirstUpdate = true;

   public override void Initialize()
   {
      Current = true;
   }

   public void OnEnter()
   {
      int idx = 0;
      foreach (var block
         in _uiReference.previousOpenedBlock.childsContainsDummy)
      {
         Tween tween = block.SetVisible(false);
         if (idx == 0)
         {
            tween.OnComplete(() =>
            {
               OnExit();
            });
         }

         ++idx;
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

   public void OnExit()
   {
      isFirstUpdate = true;
      Current = false;
   }


}
