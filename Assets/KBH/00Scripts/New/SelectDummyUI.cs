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

   
   private Tween _scalingTween;
   [Header("Additional Settings")]
   [SerializeField] private Vector2 additionalSize = new Vector2(30, 30);

   private void Awake()
   {
      _rectTrm = transform as RectTransform;
      _image = GetComponent<Image>();
   }


   public void SetScaleZero(float time)
   {
      _rectTrm.DOSizeDelta(Vector2.zero, time);
      _image.DOFade(0, time);
   }

   public void SetTrm(RectTransform targetTrm,  float time)
   {
      if (_scalingTween != null && _scalingTween.active)
         _scalingTween.Complete();

      _image.DOFade(1, time);
      _rectTrm.DOMove(targetTrm.position, time);
      _rectTrm.DOSizeDelta(targetTrm.sizeDelta + additionalSize, time);
   }


}
