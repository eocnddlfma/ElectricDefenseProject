using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewCanvas : UIAgent
{
   [Header("Resource UI")]
   [SerializeField] private Image coinGuage;
   [SerializeField] private TextMeshProUGUI coinGuageText;
   [SerializeField] private TextMeshProUGUI expText;
   [Header("Wave UI")]
   [SerializeField] private TextMeshProUGUI waveGuageText;
   [SerializeField] private Image waveGuage;
   [Header("Setting Panel")]
   [SerializeField] private RectTransform settingPanel;
   [SerializeField] private Button settingBtn;

   public float CoinGuagePercent
   {
      get => coinGuage.fillAmount;
      set => coinGuage.fillAmount = Mathf.Clamp01(value);
   }

   public string CoinText
   {
      get => coinGuageText.text;
      set => coinGuageText.text = value;
   }

   public string ExpText
   {
      get => expText.text;
      set => expText.text = value;
   }

   public float WaveGuagePercent
   {
      get => waveGuage.fillAmount;
      set => waveGuage.fillAmount = Mathf.Clamp01(value);
   }

   private void Awake()
   {
      settingBtn.onClick.AddListener(OpenSettingPanel);
   }

   private void OpenSettingPanel()
   {
      // 여기서 세팅 창을 열어줍니다. 
   }



}
