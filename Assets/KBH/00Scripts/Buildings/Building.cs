using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building<T> : Agent, IBuildingAgent
   where T : Enum
{
   [Header("BuildingInfo")]
   [SerializeField] protected BuildingInfo _buildingInfoSO;

   [Header("Compoent")]
   [SerializeField] protected BuildingStateMachine<T> mainStateMachine;

   [SerializeField] protected MeshRenderer _meshRenderer;
   protected Material _material;

   public readonly int _alphaHash = Shader.PropertyToID("_Alpha");
   public readonly int _baseColorHash = Shader.PropertyToID("_BaseColor");
   public readonly int _dissolvePercentHash = Shader.PropertyToID("_DissolvePercent");

   public bool IsRender
   {
      set => _meshRenderer.enabled = value;
   }


   public override void Awake()
   {
      mainStateMachine.Intialize(this, BuildingBaseStateEnum.Stay);
      _meshRenderer = transform.Find("Visual")
         .GetComponent<MeshRenderer>();

      health = transform.Find("HealthBar").GetComponent<Health>();
      damageCaster = GetComponent<DamageCaster>();
      damageCaster.Initialize(this);
      _material = _meshRenderer.material;
      base.Awake();
   }


   public override void WakeUpAction()
   {
      _material.SetFloat(_alphaHash, 0);
      _material.SetColor(_baseColorHash, _buildingInfoSO.baseColor);
      _material.SetFloat(_dissolvePercentHash, 1);

      float showTime = 2f;
      _material.DOFloat(0, _dissolvePercentHash, showTime);
      _material.DOFloat(1, _alphaHash, showTime);

      IsRender = true;
   }


   public virtual void Die()
   {
      Destroy(collider);
      float dissolveTime = 2f;
      MapUtil.RemoveAgent(this);
      _material.DOFloat(1, _dissolvePercentHash, dissolveTime);
      _material.DOFloat(0, _alphaHash, dissolveTime)
         .OnComplete(() =>
         {
            BuildingUtil.Push(this);
         });
   }

   public virtual void Upgrade(){}
   public virtual void ShowDebug(){}
   public virtual void HideDebug(){}

   public virtual void UpdateBuilding(){}
}
