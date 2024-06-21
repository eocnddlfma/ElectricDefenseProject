using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DecideUIBlock : BaseAgent, IEnumerable<DecideUIBlock>
{
   private CanvasGroup _canvasGroup;

   [SerializeField] private float fadeTransitionTime = 0.2f;
   [SerializeField] private float fadeAdditionalTimePerIdx = 0.05f;

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
   private Vector3 startPosition;

   public override void Initialize()
   {
      startPosition = transform.position;
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


   public Tween SetVisible(bool isVisible, int idx = 0)
   {
      float positionDownValue = 200;

      float totalTransitionTime = fadeTransitionTime
            + idx * fadeAdditionalTimePerIdx;


      

      if (_canvasGroup is null) 
         return DOVirtual.DelayedCall(totalTransitionTime, null);

      _canvasGroup.interactable = isVisible;
      _canvasGroup.blocksRaycasts = isVisible;
      
      if (isVisible)
      {
         transform.position = startPosition + Vector3.down * positionDownValue;

         transform.DOMove(startPosition,
            totalTransitionTime);

         return _canvasGroup.DOFade(1, totalTransitionTime);
      }
      else
      {
         transform.position = startPosition;

         transform.DOMove(startPosition + Vector3.down * positionDownValue,
            totalTransitionTime);

         return _canvasGroup.DOFade(0, totalTransitionTime);
      }

   }

   public IEnumerator<DecideUIBlock> GetEnumerator()
      => childsContainsDummy.GetEnumerator();

   IEnumerator IEnumerable.GetEnumerator()
      => childsContainsDummy.GetEnumerator();
}
