using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAgent : MonoBehaviour
{
   [SerializeField] private MaskableGraphic graphic;
   [SerializeField] private CanvasGroup canvasGroup;

   private float alpha;
   public float Alpha
   {
      get => alpha;
      set
      {
         if (graphic)
            graphic.color *= new Color(1,1,1,value);
         if (canvasGroup)
            canvasGroup.alpha = value;

         alpha = value;
      }
   }

   private bool isSelectable;
   public bool IsSelectable
   {
      get => isSelectable;
      set
      {
         if (graphic)
            graphic.raycastTarget = value;

         if (canvasGroup)
         {
            canvasGroup.blocksRaycasts = value;
            canvasGroup.interactable = value;
         }
      }
   }


}
