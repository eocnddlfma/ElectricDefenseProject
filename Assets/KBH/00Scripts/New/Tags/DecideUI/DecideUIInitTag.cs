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
      foreach (var block
         in _uiReference.currentOpenedBlock.childsContainsDummy)
      {
         Tween tween = block.SetVisible(true);
         if (idx == 0)
         {
            tween.OnComplete(() =>
            {
               Debug.Log("ÇÏ±ä ÇÔ ;;");
               Exit();
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

   public void Exit()
   {
      isFirstUpdate = true;
      Current = false;
   }

}
