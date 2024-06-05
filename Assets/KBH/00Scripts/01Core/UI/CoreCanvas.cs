using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoreCanvas : UIAgent
{
   [SerializeField] private Button _modeSwapBtn;
   [SerializeField] private TextMeshProUGUI _modeShowText;
   
   private int currentIdx = 0;
   public GameMode currentMode;
   public event Action<GameMode> OnModeChangeEvent;

   [SerializeField] private GameMode[] _modeNames;
   

   private void Awake()
   {
      _modeSwapBtn.onClick.AddListener(OnModeChange);
   }

   private void OnModeChange()
   {
      currentIdx = (currentIdx + 1) % _modeNames.Length;
      currentMode = _modeNames[currentIdx];

      _modeShowText.text
         = @$"Mode
<b>{_modeNames[currentIdx]}</b>";

      OnModeChangeEvent?.Invoke(currentMode);
   }



}
