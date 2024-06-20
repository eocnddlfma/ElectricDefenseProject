using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DecideUIBlock : BaseAgent
{
   private CanvasGroup _canvasGroup;

   private float fadeTransitionTime = 1f;

   #region Hide
   [HideInInspector] public DecideUIBlock parentBlock;
   [HideInInspector] public List<DecideUIBlock> childs;
   [HideInInspector] public List<DecideUIBlock> childsContainsDummy;
   [HideInInspector] public RectTransform visualTrm;
   #endregion


   public int childCount => childs is not null ? childs.Count : 0;
   public DecideUIBlock this[int idx]
   {
      get
      {
         DecideUIBlock result = null;
         if(idx >= 0 && idx < childCount)
         {
            result = childs[idx];
         }

         return result;
      }
   }

   public bool isInteractable = true;


   public override void Initialize()
   {
      visualTrm = transform.Find("Visual") as RectTransform;
      
      if(visualTrm)
         _canvasGroup = visualTrm.GetComponent<CanvasGroup>();

      parentBlock = transform.parent.GetComponent<DecideUIBlock>();
      childs = new List<DecideUIBlock>();

      for(int i = 0; i<transform.childCount; ++i)
      {
         var childIdxCompo
            = transform.GetChild(i).GetComponent<DecideUIBlock>();

         if (childIdxCompo is not null)
         {
            childsContainsDummy.Add(childIdxCompo);
            childIdxCompo.Initialize();

            if (childIdxCompo.isInteractable)
            {
               childs.Add(childIdxCompo);
            }
         }

      }
   }

   public override void BaseUpdate()
   {
      
   }


   public Tween SetVisible(bool isVisible)
   {
      if (_canvasGroup is null) 
         return DOVirtual.DelayedCall(fadeTransitionTime, null);

      _canvasGroup.interactable = isVisible;
      _canvasGroup.blocksRaycasts = isVisible;
      
      if (isVisible)
      {
         return _canvasGroup.DOFade(1, fadeTransitionTime);
      }
      else
      {
         return _canvasGroup.DOFade(0, fadeTransitionTime);
      }

   }


}
