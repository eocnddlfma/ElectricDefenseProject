using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UIValueTypeEnum
{
   percent,
   value
}

public class ValueUIBlock : DecideUIBlock
{
   public UIValueTypeEnum valueType;
   public float valueChangeSpeed = 1f;
   [Header("Percent")]
   [Range(0f, 1f)] private float percent = 0;
   [SerializeField] private Image gauge;
   [SerializeField] private bool isGaugeRenderWithFloor = false;
   [SerializeField] private int floorUnit;

   [Header("Value")]
   [SerializeField] private float value = 0;
   [SerializeField] private float minValue = -10;
   [SerializeField] private float maxValue = 10;
   public TextMeshProUGUI textRenderer;

   [Header("PlayerPrefs Save")]
   public string saveName;


   public float Percent
   {
      get => percent;
      set
      {
         percent = Mathf.Clamp01(value);

         if (gauge)
            gauge.fillAmount = isGaugeRenderWithFloor ?
               Mathf.Floor(percent * floorUnit) / floorUnit :
               percent;

         if (textRenderer)
         {
            textRenderer.text = $"{value.ToString()}";
         }
      }
   }

   public float Value
   {
      get => value;
      set
      {
         this.value = Mathf.Clamp(value, minValue, maxValue);

         if (gauge)
         {
            float gaugePercent = Mathf.InverseLerp(minValue, maxValue, value);
            gauge.fillAmount = isGaugeRenderWithFloor ?
               Mathf.Floor(gaugePercent * floorUnit) / floorUnit :
               gaugePercent;

         }

         if (textRenderer)
            textRenderer.text = $"{Mathf.FloorToInt(value).ToString()}";
      }
   }

   private void Awake()
   {
      switch (valueType)
      {
         case UIValueTypeEnum.percent:
            Percent = PlayerPrefs.GetFloat(saveName, 0f);
            break;

         case UIValueTypeEnum.value:
            Value = PlayerPrefs.GetFloat(saveName, 0f);
            break;
      }
   }

}
