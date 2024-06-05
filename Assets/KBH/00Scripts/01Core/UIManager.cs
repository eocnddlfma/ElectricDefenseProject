using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
   private GameMode _previousMode;

   public CoreCanvas coreCanvas;
   public BuildCanvas buildCanvas;
   public ViewCanvas viewCanvas;

   private void Awake()
   {
      coreCanvas = transform.Find("CoreCanvas").GetComponent<CoreCanvas>();
      buildCanvas = transform.Find("BuildCanvas").GetComponent<BuildCanvas>();
      viewCanvas = transform.Find("ViewCanvas").GetComponent<ViewCanvas>();
      coreCanvas.OnModeChangeEvent += OnModeChange;
      _previousMode = GameMode.View;
   }

   private void OnModeChange(GameMode currentMode)
   {
      switch (currentMode)
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

      _previousMode = currentMode;
   }
}
