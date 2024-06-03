using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoreCanvas : UIAgent
{
   public Button modeSwapBtn;
   [SerializeField] private TextMeshProUGUI _modeShowText;
   public GameMode currentMode;

   [SerializeField] private GameMode[] _modeNames;

   private void Awake()
   {
      modeSwapBtn.onClick.AddListener(OnModeChange);
   }

   private void OnModeChange()
   {
      currentMode = (GameMode)(((int)currentMode + 1) % _modeNames.Length);
      _modeShowText.text
         = @$"Mode
<b>{_modeNames[(int)currentMode]}</b>";

   }



}
