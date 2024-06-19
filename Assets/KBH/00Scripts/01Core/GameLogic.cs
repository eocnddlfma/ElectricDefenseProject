using System;
using UnityEngine;

[Serializable]
public class GameLogic : BaseAgent
{
   [Space(35), Header("Player Setting")]
   [SerializeField] private PlayerAgent playerBowl;
   [SerializeField] private Player _player;
   
   [Space(35), Header("Select Setting")]
   private Vector2Int _previousSelectedBuildingCellPosition;
   private Vector3 _previousSelectedBuildingPosition;
   [SerializeField] private Agent _selectedAgent = null;
   [SerializeField] private MoveBuildingOrder _currentMoveOrder;
   

   public override void Initialize()
   {
      InputUtil.Instance.OnClickEvent += OnClickHandle;
   }

   private void OnClickHandle(bool isClickDown)
   {
      if (isClickDown && _selectedAgent is null)
      {
         _selectedAgent = Shot3DUtil.GetAgentOnCurrentCursor();
         if (_selectedAgent is not null)
         {
            _previousSelectedBuildingPosition = _selectedAgent.transform.position;
            _previousSelectedBuildingCellPosition = _selectedAgent.cellPosition;
            _currentMoveOrder = MoveBuildingOrder.Grab;
         }
      }
      else
      {
         _currentMoveOrder = MoveBuildingOrder.None;
      }
   }




   private void HandleEndUpgradeEvent()
   {
      currentUpgradeBuilding.HideDebug();
   }

   IBuildingAgent currentUpgradeBuilding;
   private Transform HandleUpgradeEvent(Vector2Int position)
   {
      currentUpgradeBuilding = MapUtil.Instance[position] as IBuildingAgent;
      if (currentUpgradeBuilding is not null)
      {
         currentUpgradeBuilding.ShowDebug();
         return MapUtil.Instance[position].transform;
      }
      else
      {
         return null;
      }
   }

   private void HandleEraseEvent(Vector2Int erasePosition)
   {
      (MapUtil.Instance[erasePosition] as IBuildingAgent).Die();
      UpdateBuildings();
   }


   private void HandleMoveStartEvent()
   {
      if (!_selectedAgent) return;

      MapUtil.RemoveAgent(_selectedAgent);
   }

   private void HandleMoveDuringEvent()
   {
      if (!_selectedAgent) return;

      _selectedAgent.transform.position
         = Shot3DUtil.DrawingMeshPosition;
   }

   private void HandleMoveEndEvent()
   {
      if (_selectedAgent)
      {
         if (Shot3DUtil.cursorCellType == AgentType.None)
         {
            _selectedAgent.cellPosition = Shot3DUtil.cursorCellPosition;
            MapUtil.RegisteAgent(_selectedAgent);

            Vector3 resultPos = Shot3DUtil.DrawingMeshPosition;

            resultPos.y = _previousSelectedBuildingPosition.y;
            _selectedAgent.transform.position = resultPos;

         }
         else
         {
            _selectedAgent.cellPosition = _previousSelectedBuildingCellPosition;
            MapUtil.RegisteAgent(_selectedAgent);

            _selectedAgent.transform.position = _previousSelectedBuildingPosition;
         }

         _selectedAgent = null;
      }
   }

   private void HandleBuildEvent(AgentType type, Vector2Int position)
   {
      Vector3 spawnPosition = Shot3DUtil.DrawingMeshPosition;
      Agent building
         = BuildingUtil.Pop(type, Shot3DUtil.cursorCellPosition);

      MapUtil.RegisteAgent(building);

      building.WaitForWakeUp(() =>
      {
         building.transform.position = spawnPosition;
         UpdateBuildings();
      });
   }
   private void UpdateBuildings()
   {
      foreach (var agent in MapUtil.Instance.agentRegisteDictionary)
      {
         bool isAgentExist = agent.Value;
         bool isGoldMinor = agent.Value.agentType == AgentType.GoldMinor;
         bool isIBuildingAgent = agent.Value is IBuildingAgent;

         if (isAgentExist && isGoldMinor && isIBuildingAgent)
         {
            (agent.Value as IBuildingAgent).UpdateBuilding();
         }
      }

      foreach (var agent in MapUtil.Instance.agentRegisteDictionary)
      {
         bool isAgentExist = agent.Value;
         bool isNormalBuilding = agent.Value.agentType != AgentType.GoldMinor;

         bool isIBuildingAgent = agent.Value is IBuildingAgent;


         if (isAgentExist && isNormalBuilding && isIBuildingAgent)
         {
            (agent.Value as IBuildingAgent).UpdateBuilding();
         }
      }
   }

   public override void Update()
   {
   }


}
