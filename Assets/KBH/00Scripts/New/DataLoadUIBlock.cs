using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataLoadUIBlock : DecideUIBlock
{
   [SerializeField] private string loadDataName;
   [SerializeField] private TextMeshProUGUI renderTextMeshTarget;

   private void Awake()
   {
      RenderUpdate();
   }

   public void RenderUpdate()
   {
      renderTextMeshTarget
               .text = PlayerPrefs.GetFloat(loadDataName, 0).ToString();
   }
}
