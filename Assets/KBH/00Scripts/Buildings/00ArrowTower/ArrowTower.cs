using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Flags]
public enum ArrowTowerType
{
   Arrow_Num_Many = 1, // 1
   Stun_Effect = 2, // 2, 1-2
   Slow_Energy = 4, // 1-1
   Boom_Splash = 8, // 2-1
   Sniping = 16 // 2-2
}

[System.Serializable]
public class ArrowTypeTree
{
   public ArrowTowerType baseType;
   public ArrowTypeTree[] nextTypes;
   public Sprite image;
   public string showText;
}

public class ArrowTower : Building<ArrowTowerStateEnum>
{
   public ArrowTowerType currentType;
   public ArrowTypeTree[] typeTree;
   private ArrowTypeTree currentTree;
   [SerializeField] private GameObject _rangeDebugger;

   [SerializeField] private Transform _first;
   [SerializeField] private Transform _second;


   [SerializeField] private Image _firstUpgradeImage;
   [SerializeField] private TextMeshProUGUI _firstUpgradeText;
   [SerializeField] private Image _secondUpgradeImage;
   [SerializeField] private TextMeshProUGUI _secondUpgradeText;

   [SerializeField] private TextMeshProUGUI _alreadySetText;
   [SerializeField] private CanvasGroup _canvasGroup;

   [SerializeField] private Button _firstUpgradeBtn;
   [SerializeField] private Button _secondUpgradeBtn;

   bool isAlreadyUpgraded = false;

   [SerializeField] private ArrowTowerStateReference _stayStateReference;
   [SerializeField] private GameObject _bulbAgent;

   public override void Awake()
   {
      base.Awake();
      _canvasGroup = transform.Find("LevelTreeCanvas")
         .GetComponent<CanvasGroup>();

      _firstUpgradeBtn.onClick.AddListener(FirstBlankUpgradeEvent);
      _secondUpgradeBtn.onClick.AddListener(SecondBlankUpgradeEvent);

       _stayStateReference = mainStateMachine.stayStateSubMachine
            .Reference as ArrowTowerStateReference;
   }

   public override void Update()
   {
      base.Update();
      switch (mainStateMachine.stayStateSubMachine.State)
      {
         case ArrowTowerStateEnum.Stay:
            
            break;

         case ArrowTowerStateEnum.Attack:
            AttackAction(currentType);
            break;
      }
   }


   private void AttackAction(ArrowTowerType currentType)
   {
      StatusInitalize(currentType);
      DoAttack();
   }

   float currentTimeStamp;
   private void DoAttack()
   {
      if ((currentTimeStamp + _stayStateReference.coolTime)
         < Time.time)
      {
         if (!_stayStateReference.closestEnemy) return;

         GameObject obj = Instantiate(_bulbAgent, transform.position, Quaternion.identity);
         obj.transform.DOMove
            (_stayStateReference.closestEnemy.transform.position, 
            1.5f);

         Destroy(obj, 1.5f);
         _stayStateReference.closestEnemy.health.DoDamage(1, 1.5f);
         currentTimeStamp = Time.time;
      }


   }

   private void StatusInitalize(ArrowTowerType currentType)
   {
      if (currentType == 0)
      {
         _stayStateReference.hasBoomSplash = false;
         _stayStateReference.baseDetectDistance = 3;
         _stayStateReference.arrowPerSecond = 2;
      }

      if ((currentType & ArrowTowerType.Arrow_Num_Many) > 0)
      {
         _stayStateReference.arrowPerSecond = 10;
      }

      if ((currentType & ArrowTowerType.Boom_Splash) > 0)
      {
         _stayStateReference.hasBoomSplash = true;
         _stayStateReference.boomSplashRange = 3f;
      }

      if ((currentType & ArrowTowerType.Slow_Energy) > 0)
      {
         // 아직 구현 중
      }

      if ((currentType & ArrowTowerType.Sniping) > 0)
      {
         _stayStateReference.baseDetectDistance = 10;
         // 아직 구현 중
      }

      if ((currentType & ArrowTowerType.Stun_Effect) > 0)
      {
         // 아직 구현 중
      }
   }

   private void SecondBlankUpgradeEvent()
   {
      if (currentTree is null)
      {
         currentTree = typeTree[1];
      }
      else
      {
         currentTree = currentTree.nextTypes[1];
      }

      ShowDebug();
   }

   private void FirstBlankUpgradeEvent()
   {
      if(currentTree is null)
      {
         currentTree = typeTree[0];
      }
      else
      {
         currentTree = currentTree.nextTypes[0];
      }
      currentType |= currentTree.baseType;
      ShowDebug();
   }

   public override void WakeUpAction()
   {
      base.WakeUpAction();
      currentTree = null;
   }

   public override void ShowDebug()
   {
      _canvasGroup.interactable = true;
      _canvasGroup.blocksRaycasts = true;
      _canvasGroup.alpha = 1;

      _rangeDebugger.SetActive(true);

      if (isAlreadyUpgraded) return;
      if (currentTree is null)
      {
         _firstUpgradeImage.sprite = typeTree[0].image;
         _secondUpgradeImage.sprite = typeTree[1].image;
         _firstUpgradeText.text = typeTree[0].showText;
         _secondUpgradeText.text = typeTree[1].showText;
      }
      else
      {
         if(currentTree.nextTypes is not null
            && currentTree.nextTypes.Length > 0)
         {
            _firstUpgradeImage.sprite = currentTree.nextTypes[0].image;
            _secondUpgradeImage.sprite = currentTree.nextTypes[1].image;
            _firstUpgradeText.text = currentTree.nextTypes[0].showText;
            _secondUpgradeText.text = currentTree.nextTypes[1].showText;
         }
         else
         {
            _first.gameObject.SetActive(false);
            _second.gameObject.SetActive(false);

            _alreadySetText.gameObject.SetActive(true);

            isAlreadyUpgraded = true;
         }

      }
   }

   public override void HideDebug()
   {
      _canvasGroup.interactable = false;
      _canvasGroup.blocksRaycasts = false;
      _canvasGroup.alpha = 0;

      _rangeDebugger.SetActive(false);
   }

}
