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

   private string[] _modeNames;
   private int _modeCount;

   private void Awake()
   {
      _modeNames = Enum.GetNames(typeof(GameMode));
      _modeCount = _modeNames.Length;

      modeSwapBtn.onClick.AddListener(OnModeChange);
      OnModeChange();
   }

   private void OnModeChange()
   {
      currentMode = (GameMode)(((int)currentMode + 1) % _modeCount);
      _modeShowText.text
         = @$"Mode
<b>{_modeNames[(int)currentMode]}</b>";

   }



}
