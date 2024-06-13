using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighWall : Building<HighWallStateEnum>
{
   private bool isCicleActive = true;
   private float circleRadius = 0.5f;
   private Tween _circleScalingTween = null;

   [SerializeField] private MeshRenderer _rightRenderer;
   [SerializeField] private MeshRenderer _leftRenderer;
   [SerializeField] private MeshRenderer _downRenderer;
   [SerializeField] private MeshRenderer _upRenderer;

   public override void WakeUpAction()
   {
      base.WakeUpAction();
   }

   public override void UpdateBuilding()
   {
      base.UpdateBuilding();
      bool isRight = MapUtil.Instance[cellPosition + Vector2Int.right] is not null;
      bool isLeft = MapUtil.Instance[cellPosition + Vector2Int.left] is not null;
      bool isUp = MapUtil.Instance[cellPosition + Vector2Int.up] is not null;
      bool isDown = MapUtil.Instance[cellPosition + Vector2Int.down] is not null;

      int count = GetTrueCount(isRight, isLeft, isUp, isDown);

      _rightRenderer.enabled = isRight;
      _leftRenderer.enabled = isLeft;
      _downRenderer.enabled = isDown;
      _upRenderer.enabled = isUp;

      if (count == 2 && isCicleActive)
      {
         if ((isRight && isLeft)
            || (isUp && isDown))
         {
            if (_circleScalingTween != null && _circleScalingTween.active)
               _circleScalingTween.Kill();
         }
      }
      else if (!isCicleActive)
      {
         if (_circleScalingTween != null && _circleScalingTween.active)
            _circleScalingTween.Kill();
      }

   }

   private int GetTrueCount(params bool[] booleanList)
   {
      int cnt = 0;
      foreach (bool value in booleanList)
      {
         if (value) ++cnt;
      }
      return cnt;
   }
}
