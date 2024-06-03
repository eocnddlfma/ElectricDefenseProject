using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
   public CoreCanvas coreCanvas;
   public BuildCanvas buildCanvas;
   public ViewCanvas viewCanvas;

   private void Awake()
   {
      coreCanvas = transform.Find("CoreCanvas").GetComponent<CoreCanvas>();
      buildCanvas = transform.Find("BuildCanvas").GetComponent<BuildCanvas>();
      viewCanvas = transform.Find("ViewCanvas").GetComponent<ViewCanvas>();
      coreCanvas.modeSwapBtn.onClick.AddListener(OnModeChange);
   }

   private void OnModeChange()
   {
      switch (coreCanvas.currentMode)
      {
         case GameMode.View:
            DOTween.To(() => buildCanvas.Alpha, x => buildCanvas.Alpha = x, 0, 0.2f);
            DOTween.To(() => viewCanvas.Alpha, x => viewCanvas.Alpha = x, 1, 0.2f);
            break;

         case GameMode.Build:
            DOTween.To(() => buildCanvas.Alpha, x => buildCanvas.Alpha = x, 1, 0.2f);
            DOTween.To(() => viewCanvas.Alpha, x => viewCanvas.Alpha = x, 0, 0.2f);
            break;
      }
   }
}
