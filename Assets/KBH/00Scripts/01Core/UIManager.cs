using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            buildCanvas.Alpha = 0;
            viewCanvas.Alpha = 1;
            break;

         case GameMode.Build:
            buildCanvas.Alpha = 1;
            viewCanvas.Alpha = 0;
            break;
      }
   }
}
