using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData
{
   public float arrowDir;
   public float nextSelectDir;
}

[System.Serializable]
public class SelectInput
{
   [SerializeField] private float _inputTimer = 0;
   [SerializeField] private bool _isVertical;
   
   private InputData _data;
   public InputData Data => _data;


   public void Initialize(bool isVertical)
   {
      _data = new InputData();
      _isVertical = isVertical;
   }

   public void UpdateInput()
   {
      EnterInput();
      ValueInput();
   }

   private void EnterInput()
   {
   }

   private void ValueInput()
   {

      float inp = 0;
      if (!_isVertical)
      {
         inp = Input.GetAxisRaw("Vertical");
      }
      else
      {
         inp = Input.GetAxisRaw("Horizontal");
      }
      _data.arrowDir = inp;

      float inp1 = 0;
      if (_isVertical)
      {
         inp1 = Input.GetAxisRaw("Vertical");
      }
      else
      {
         inp1 = Input.GetAxisRaw("Horizontal");
      }
      _data.nextSelectDir = inp1;

      if (Mathf.Abs(inp) > 0)
      {
         _inputTimer = Time.time;
      }

      if (Mathf.Abs(inp1) > 0)
      {
         _inputTimer = Time.time;
      }
   }

}
