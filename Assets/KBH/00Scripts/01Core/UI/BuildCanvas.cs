using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildCanvas : UIAgent
{

   
   [System.Serializable]
   struct ToolBarPostInfo
   {
      public ToolBarEnum toolType;
      public Sprite toolBarSprite;
   }

   [System.Serializable]
   struct BuildingPostInfo
   {
      public AgentType agentType;
      public Sprite buildingSprite;
   }


   [Header("ToolBar")]
   [SerializeField] private RectTransform _toolbarContainerTrm;
   [SerializeField] private RectTransform _toolbarSpotLightImageTrm;
   [SerializeField] private List<ToolBarPostInfo> _toolbarPostInfos;
   private Dictionary<ToolBarEnum, RectTransform> _toolbarPostInfoDictionary;

   [Header("BuildingList")]
   [SerializeField] private RectTransform _buildingContainerTrm;
   private Vector3 _buildingContainerStartPosition;
   [SerializeField] private RectTransform _buildingSpotLightImageTrm;
   [SerializeField] private List<BuildingPostInfo> _buildingInfos;
   private Dictionary<AgentType, RectTransform> _buildingInfoDictionary;


   private ToolBarEnum currentToolbarType;
   public ToolBarEnum CurrentToolbarType
   {
      get => currentToolbarType;
      set
      {
         currentToolbarType = value;
         _toolbarSpotLightImageTrm.transform.position
            = _toolbarPostInfoDictionary[value].position;
      }
   }


   private AgentType currentBuildingType;
   public AgentType CurrentBuildingType
   {
      get => currentBuildingType;
      set
      {
         currentBuildingType = value;
         _buildingSpotLightImageTrm.transform.position
            = _buildingInfoDictionary[value].position;
      }
   }


   private bool isBuildingBarActive = true;
   public bool IsBuildingBarActive
   {
      get => IsBuildingBarActive;
      set
      {
         if(isBuildingBarActive != value)
         {
            float transitionTime = 0.25f;
            if (value)
            {
               _buildingContainerTrm
                  .DOMove(_buildingContainerStartPosition, transitionTime);
            }
            else
            {
               _buildingContainerTrm
                  .DOAnchorPos(_buildingContainerTrm.sizeDelta.y * Vector2.down
                  , transitionTime);
            }

            isBuildingBarActive = value;
         }
      }
   }


   private void Awake()
   {
      _toolbarPostInfoDictionary = new Dictionary<ToolBarEnum, RectTransform>();
      for(int i = 0; i<_toolbarPostInfos.Count; ++i)
      {
         ToolBarEnum toolType = _toolbarPostInfos[i].toolType;
         string toolTypeStr = toolType.ToString();

         RectTransform iconTrm = _toolbarContainerTrm.Find($"{toolTypeStr}/Icon") as RectTransform;
         Image iconImg = iconTrm.GetComponent<Image>();

         iconImg.sprite = _toolbarPostInfos[i].toolBarSprite;
         _toolbarPostInfoDictionary[_toolbarPostInfos[i].toolType] = iconTrm;

         Button button = _toolbarContainerTrm.Find(toolTypeStr).GetComponent<Button>();
         button.onClick.AddListener(() => 
         {
            CurrentToolbarType = toolType;
         });

      }

      CurrentToolbarType = ToolBarEnum.Add;


      _buildingInfoDictionary = new Dictionary<AgentType, RectTransform>();
      for (int i = 0; i < _buildingInfos.Count; ++i)
      {
         AgentType buildingType = _buildingInfos[i].agentType;
         string buildingTypeStr = buildingType.ToString();

         RectTransform buildingtypeTrm = _buildingContainerTrm.Find(buildingTypeStr) as RectTransform;
         RectTransform iconTrm = buildingtypeTrm.Find("Icon") as RectTransform;
         RectTransform centerTrm = buildingtypeTrm.Find("Center") as RectTransform;
         Image iconImg = iconTrm.GetComponent<Image>();
         TextMeshProUGUI iconText = buildingtypeTrm.Find("Text").GetComponent<TextMeshProUGUI>();

         iconImg.sprite = _buildingInfos[i].buildingSprite;
         _buildingInfoDictionary[buildingType] = centerTrm;
         iconText.text = buildingTypeStr;

         Button button = _buildingContainerTrm.Find(buildingTypeStr).GetComponent<Button>();
         button.onClick.AddListener(() =>
         {
            CurrentBuildingType = buildingType;
         });

      }

      CurrentBuildingType = AgentType.ArrowTower;

      _buildingContainerStartPosition = _buildingContainerTrm.position;
   }

}
