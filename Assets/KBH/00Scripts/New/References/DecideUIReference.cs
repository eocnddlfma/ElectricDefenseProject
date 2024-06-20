using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecideUIReference : MonoBehaviour
{
   private SelectInput _selectInput;
   public InputData currentInput;
   
   public DecideUIBlock uiBlockRoot;
   public DecideUIBlock previousOpenedBlock;
   public DecideUIBlock currentOpenedBlock;

   public void Initialize()
   {
      _selectInput = new SelectInput();
      _selectInput.Initialize(false);

      uiBlockRoot = transform.Find("Root")
         .GetComponent<DecideUIBlock>();

      uiBlockRoot.Initialize();
      currentOpenedBlock = uiBlockRoot;
   }

   public void UpdateInput()
   {
      _selectInput.UpdateInput();
      currentInput = _selectInput.Data;
   }

}
