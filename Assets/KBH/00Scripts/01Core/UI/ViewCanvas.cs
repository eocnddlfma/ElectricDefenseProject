using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ViewCanvas : UIAgent
{
   [Header("Resource UI")]
   [SerializeField] private Image coinGuage;
   [SerializeField] private TextMeshProUGUI coinGaugeText;
   [SerializeField] private TextMeshProUGUI expText;

   [Header("Wave UI")]
   [SerializeField] private TextMeshProUGUI waveGaugeText;
   [SerializeField] private Image waveGauge;
   [Header("Setting Panel")]
   [SerializeField] private RectTransform settingPanel;
   [SerializeField] private Button settingBtn;

   public float CoinGaugePercent
   {
      get => coinGuage.fillAmount;
      set => coinGuage.fillAmount = Mathf.Clamp01(value);
   }

   public string CoinText
   {
      get => coinGaugeText.text;
      set => coinGaugeText.text = value;
   }

   public string ExpText
   {
      get => expText.text;
      set => expText.text = value;
   }

   public float WaveGaugePercent
   {
      get => waveGauge.fillAmount;
      set => waveGauge.fillAmount = Mathf.Clamp01(value);
   }
   
   public string WaveText
   {
      get => waveGaugeText.text;
      set => waveGaugeText.text = "Wave : "+value;
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
