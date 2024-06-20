using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDummyUI : MonoBehaviour
{

   [Header("Components")]
   [SerializeField] private Image _image;
   [SerializeField] private RectTransform _rectTrm;

   
   [Header("Beat Scale")]
   [SerializeField] private float _scaleBeatCycleTime = 1f;
   [SerializeField] private AnimationCurve _scaleCurve;
   private Tween _scalingTween;

   [Header("Select Setting")]
   [SerializeField] private float _selectDelayTime = 0.3f;
   private Color _normalColor = Color.white;
   [SerializeField] private Color _selectColor = Color.yellow;
   [SerializeField] private float _selectScale = 1.2f;
   [SerializeField] private AnimationCurve _selectCurve;


   private void Awake()
   {
      _rectTrm = transform as RectTransform;
      _image = GetComponent<Image>();
      _normalColor = _image.color;
      BeatScalingTweenStart();

   }

   private void BeatScalingTweenStart()
   {
      if (_scalingTween != null && _scalingTween.active)
         _scalingTween.Complete();

      _scalingTween = transform.DOScale(_selectScale, _scaleBeatCycleTime)
               .SetEase(_scaleCurve)
               .SetLoops(-1, LoopType.Restart);
   }

   public void SetScaleZero(float time)
   {
      _rectTrm.DOSizeDelta(Vector2.zero, time);
   }

   public void SetTrm(RectTransform targetTrm,  float time)
   {
      if (_scalingTween != null && _scalingTween.active)
         _scalingTween.Complete();

      _rectTrm.DOMove(targetTrm.position, time);
      _rectTrm.DOSizeDelta(targetTrm.sizeDelta, time)
         .OnComplete(() =>
         {
            BeatScalingTweenStart();
         });
   }



}
