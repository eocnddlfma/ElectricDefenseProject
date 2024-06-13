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


   public override void Awake()
   {
      base.Awake();
      _canvasGroup = transform.Find("LevelTreeCanvas")
         .GetComponent<CanvasGroup>();

      _firstUpgradeBtn.onClick.AddListener(FirstBlankUpgradeEvent);
      _secondUpgradeBtn.onClick.AddListener(SecondBlankUpgradeEvent);
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
