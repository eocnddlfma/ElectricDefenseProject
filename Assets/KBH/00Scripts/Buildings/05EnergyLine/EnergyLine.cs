using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyLine : Building<EnergyLineStateEnum>
{
   private readonly int _circleRadiusHash = Shader.PropertyToID("_CircleRadius");
   private readonly int _isRightHash = Shader.PropertyToID("_IsRight");
   private readonly int _isleftHash = Shader.PropertyToID("_IsLeft");
   private readonly int _isUpHash = Shader.PropertyToID("_IsUp");
   private readonly int _isDownHash = Shader.PropertyToID("_IsDown");

   private bool isCicleActive = true;
   private float circleRadius = 0.5f;
   private Tween _circleScalingTween = null;

   public override void WakeUpAction()
   {
      base.WakeUpAction();
      circleRadius = _material.GetFloat(_circleRadiusHash);
   }

   public void UpdateLine()
   {
      bool isRight = MapUtil.Instance[cellPosition + Vector2Int.right] is not null;
      bool isLeft = MapUtil.Instance[cellPosition + Vector2Int.left] is not null;
      bool isUp = MapUtil.Instance[cellPosition + Vector2Int.up] is not null;
      bool isDown = MapUtil.Instance[cellPosition + Vector2Int.down] is not null;

      int count = GetTrueCount(isRight, isLeft, isUp, isDown);


      _material.SetFloat(_isRightHash, isRight ? 1 : 0);
      _material.SetFloat(_isleftHash, isLeft ? 1 : 0);
      _material.SetFloat(_isUpHash, isUp ? 1 : 0);
      _material.SetFloat(_isDownHash, isDown ? 1 : 0);

      if(count == 2 && isCicleActive)
      {
         if((isRight && isLeft)
            || (isUp && isDown))
         {
            if(_circleScalingTween != null && _circleScalingTween.active)
               _circleScalingTween.Kill();

            _circleScalingTween
               = _material.DOFloat(0, _circleRadiusHash, 0.2f);

            isCicleActive = false;
         }
      }
      else if(!isCicleActive)
      {
         if (_circleScalingTween != null && _circleScalingTween.active)
            _circleScalingTween.Kill();

         _circleScalingTween
            = _material.DOFloat(circleRadius, _circleRadiusHash, 0.2f);

         isCicleActive = true;
      }
      
   }

   private int GetTrueCount(params bool[] booleanList)
   {
      int cnt = 0;
      foreach(bool value in booleanList)
      {
         if (value) ++cnt;
      }
      return cnt; 
   }
}
