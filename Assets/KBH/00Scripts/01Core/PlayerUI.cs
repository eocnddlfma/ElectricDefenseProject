using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
   private GameMode _previousMode;

   public CoreCanvas coreCanvas;
   public BuildCanvas buildCanvas;
   public ViewCanvas viewCanvas;
   public ShopCanvas shopCanvas;

   private void Awake()
   {
      coreCanvas = transform.Find("CoreCanvas").GetComponent<CoreCanvas>();
      buildCanvas = transform.Find("BuildCanvas").GetComponent<BuildCanvas>();
      viewCanvas = transform.Find("ViewCanvas").GetComponent<ViewCanvas>();
      shopCanvas = transform.Find("ShopCanvas").GetComponent<ShopCanvas>();

      coreCanvas.OnModeChangeEvent += OnModeChange;
      _previousMode = GameMode.View;
   }


   public void OnModeChange(GameMode currentMode)
   {
      switch (currentMode)
      {
         case GameMode.View:
            DOTween.To(() => buildCanvas.Alpha, x => buildCanvas.Alpha = x, 0, 0.2f);
            DOTween.To(() => viewCanvas.Alpha, x => viewCanvas.Alpha = x, 1, 0.2f);
            DOTween.To(() => shopCanvas.Alpha, x => shopCanvas.Alpha = x, 0, 0.2f);
            viewCanvas.IsSelectable = true;
            buildCanvas.IsSelectable = false;
            shopCanvas.IsSelectable = false;
            break;

         case GameMode.Build:
            DOTween.To(() => buildCanvas.Alpha, x => buildCanvas.Alpha = x, 1, 0.2f);
            DOTween.To(() => viewCanvas.Alpha, x => viewCanvas.Alpha = x, 0, 0.2f);
            DOTween.To(() => shopCanvas.Alpha, x => shopCanvas.Alpha = x, 0, 0.2f);
            viewCanvas.IsSelectable = false;
            buildCanvas.IsSelectable = true;
            shopCanvas.IsSelectable = false;

            break;

         case GameMode.Upgrade:
            DOTween.To(() => buildCanvas.Alpha, x => buildCanvas.Alpha = x, 0, 0.2f);
            DOTween.To(() => viewCanvas.Alpha, x => viewCanvas.Alpha = x, 0, 0.2f);
            DOTween.To(() => shopCanvas.Alpha, x => shopCanvas.Alpha = x, 0, 0.2f);
            viewCanvas.IsSelectable = false;
            buildCanvas.IsSelectable = false;
            shopCanvas.IsSelectable = false;
            break;

         case GameMode.Shop:
            DOTween.To(() => buildCanvas.Alpha, x => buildCanvas.Alpha = x, 0, 0.2f);
            DOTween.To(() => viewCanvas.Alpha, x => viewCanvas.Alpha = x, 0, 0.2f);
            DOTween.To(() => shopCanvas.Alpha, x => shopCanvas.Alpha = x, 1, 0.2f);
            viewCanvas.IsSelectable = false;
            buildCanvas.IsSelectable = false;
            shopCanvas.IsSelectable = true;
            break;

      }

      coreCanvas.gameObject.SetActive
         (currentMode != GameMode.Upgrade && currentMode != GameMode.Shop);
      _previousMode = currentMode;
   }
}
