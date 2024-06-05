using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CursorShotVisual : MonoBehaviour
{
   [System.Serializable]
   struct CursorColorByState
   {
      public CursorShotStateEnum cursorState;
      public Color color;
   }

   private bool _isShown = false;
   public bool IsShown => _isShown;

   private MeshRenderer _meshRenderer;
   private Material _material;

   [SerializeField] private CursorColorByState[] _colorByStateList;
   private Dictionary<CursorShotStateEnum, Color> _colorByStateDictionary;

   private readonly int _baseColorHash
      = Shader.PropertyToID("_BaseColor");
   private readonly int _dissolvePercentHash
      = Shader.PropertyToID("_DissolvePercent");


   private Tween _colorChangeTween;
   [SerializeField] private CursorShotStateEnum _currentState;
   

   private void Awake()
   {
      _meshRenderer = GetComponent<MeshRenderer>();
      _material = _meshRenderer.material;

      _colorByStateDictionary = new Dictionary<CursorShotStateEnum, Color>();
      for(int i = 0; i<_colorByStateList.Length; ++i)
      {
         _colorByStateDictionary[_colorByStateList[i].cursorState]
            = _colorByStateList[i].color;
      }
      
   }

   public void SetState(CursorShotStateEnum state, float time)
   {
      if (_currentState == state) return;

      if (_colorChangeTween != null && _colorChangeTween.active)
         _colorChangeTween.Kill();

      _colorChangeTween
         = _material.DOVector(_colorByStateDictionary[state], _baseColorHash, time);

      _currentState = state;
   }

   public void SetShow(bool isShow, float time)
   {
      if (isShow == _isShown) return;

      if (isShow)
      {
         if (_colorChangeTween != null && _colorChangeTween.active)
            _colorChangeTween.Kill();

         _colorChangeTween
          = _material.DOVector(_colorByStateDictionary[_currentState], _baseColorHash, time);

         _material.DOFloat(0, _dissolvePercentHash, time);
      }
      else
      {
         _material.DOVector(Color.black, _baseColorHash, time);
         _material.DOFloat(1, _dissolvePercentHash, time);
      }

      _isShown = isShow;
   }

}
