using System;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class PlayerAgent : Agent
{
   //enum MoveBuildingOrder
   //{
   //   None,
   //   Grab,
   //   Stay,
   //}

   //[Header("Referecne")]
   //[SerializeField] private UIManager uiManager;

   //[Header("Compoents")]
   //[SerializeField] private StateMachine<GameMode> _stateMachine;
   //[SerializeField] private CursorShotVisual _cursorVisual;

   //private GameMode _previousState;

   //[Header("Movement")]
   //[SerializeField] private float _speed = 3f;

   //[Header("Building Move Action")]
   //[SerializeField] private bool isMoveBuilding = false;
   //private Vector2Int _previousSelectedBuildingCellPosition;
   //private Vector3 _previousSelectedBuildingPosition;
   //[SerializeField] private Agent _selectedAgent = null;
   //[SerializeField] private MoveBuildingOrder _currentMoveOrder;


   //[Header("Building Upgrade Action")]
   //[SerializeField] private IBuildingAgent _selectedUpgradeAgent;

   //[Header("Cam Control")]
   //[SerializeField] private CinemachineVirtualCamera _virtualCam;
   //[SerializeField] private CinemachineTransposer _camTransposer;
   //[SerializeField] private Vector3 _viewCamOffset;
   //[SerializeField] private Vector3 _UpgradeCamOffset;
   //[SerializeField] private Vector2 onUpgradeDamping;

   //public override void Awake()
   //{
   //   uiManager = FindAnyObjectByType<UIManager>();
   //   _stateMachine.Intialize(this, GameMode.View);

   //   InputUtil.Instance.OnClickEvent += OnClickHandle;
   //   _camTransposer = _virtualCam.GetCinemachineComponent<CinemachineTransposer>();
   //}

   //private void OnClickHandle(bool isClickDown)
   //{
   //   if (isClickDown && _selectedAgent is null)
   //   {
   //      _selectedAgent = Shot3DUtil.GetAgentOnCurrentCursor();
   //      if(_selectedAgent is not null)
   //      {
   //         _previousSelectedBuildingPosition = _selectedAgent.transform.position;
   //         _previousSelectedBuildingCellPosition = _selectedAgent.cellPosition;
   //         _currentMoveOrder = MoveBuildingOrder.Grab;
   //      }
   //   }
   //   else
   //   {
   //      _currentMoveOrder = MoveBuildingOrder.None;
   //   }
   //}

   //private void Update()
   //{
   //   _stateMachine.Update();

   //   if (_previousState != _stateMachine.State)
   //   {
   //      OnStateChange(_previousState, _stateMachine.State);
   //   }
   //   UpdateGameMode(_stateMachine.State);

   //   _previousState = _stateMachine.State;
   //}

   //private void UpdateGameMode(GameMode currentState)
   //{
   //   switch (currentState)
   //   {
   //      case GameMode.View:
   //         Shot3DUtil
   //            .SetCursorShotVisual(ToolBarEnum.None);
   //         MoveAction();
   //         break;

   //      case GameMode.Build:
   //         BuildAction();
   //         MoveAction();
   //         break;

   //      case GameMode.Upgrade:
   //         UpgradeAction();
   //         break;

   //      case GameMode.Shop:
   //         break;

   //   }

   //}

   //private void UpgradeAction()
   //{
   //}

   //private void BuildAction()
   //{
   //   Shot3DUtil
   //      .SetCursorShotVisual(uiManager.buildCanvas.CurrentToolbarType);

   //   switch (uiManager.buildCanvas.CurrentToolbarType)
   //   {
   //      case ToolBarEnum.None:
   //         break;

   //      case ToolBarEnum.Add:
   //         AddBuildingAction();
   //         break;

   //      case ToolBarEnum.Remove:
   //         RemoveBuildingAction();
   //         break;

   //      case ToolBarEnum.Move:
   //         MoveBuildingAction();
   //         break;
   //   }

   //   bool isAdd = uiManager.buildCanvas.CurrentToolbarType == ToolBarEnum.Add;
   //   uiManager.buildCanvas.IsBuildingBarActive = isAdd;
   //}

   //private void MoveBuildingAction()
   //{
   //   CursorShotStateEnum cursorShotState = CursorShotStateEnum.Default;
   //   Shot3DUtil.SetDrawingMesh(AgentType.None);

   //   switch (_currentMoveOrder)
   //   {
   //      case MoveBuildingOrder.Grab:
   //         cursorShotState = CursorShotStateEnum.CanBuild;
   //         MapUtil.RemoveAgent(_selectedAgent);
   //         _currentMoveOrder = MoveBuildingOrder.Stay;
   //         break;

   //      case MoveBuildingOrder.Stay:

   //         if(Shot3DUtil.cursorCellType == AgentType.None)
   //         {
   //            cursorShotState = CursorShotStateEnum.CanBuild;
   //         }
   //         else
   //         {
   //            cursorShotState = CursorShotStateEnum.ImpossibleBuild;
   //         }
   //         _selectedAgent.transform.position
   //            = Shot3DUtil.DrawingMeshPosition;

   //         break;
   //      case MoveBuildingOrder.None:
   //         if(_selectedAgent is not null)
   //         {
   //            if (Shot3DUtil.cursorCellType == AgentType.None)
   //            {
   //               _selectedAgent.cellPosition = Shot3DUtil.cursorCellPosition;
   //               MapUtil.RegisteAgent(_selectedAgent);

   //               Vector3 resultPos = Shot3DUtil.DrawingMeshPosition;

   //               resultPos.y = _previousSelectedBuildingPosition.y;
   //               _selectedAgent.transform.position = resultPos;


   //               if (_selectedAgent is EnergyLine)
   //                  (_selectedAgent as EnergyLine).UpdateLine();
   //               if (_selectedAgent is HighWall)
   //                  (_selectedAgent as HighWall).UpdateWall();

   //               Vector2Int updatePosition = Shot3DUtil.cursorCellPosition;

   //               foreach (Vector2Int direction in MapHelper.FourDirection)
   //               {
   //                  Agent targetAgent = MapUtil.Instance[updatePosition + direction];
   //                  if (targetAgent is EnergyLine)
   //                  {
   //                     (targetAgent as EnergyLine).UpdateLine();
   //                  }
   //                  if (targetAgent is HighWall)
   //                  {
   //                     (targetAgent as HighWall).UpdateWall();
   //                  }
   //               }
                  
   //               Vector2Int updatePosition2 = _previousSelectedBuildingCellPosition;

   //               foreach (Vector2Int direction in MapHelper.FourDirection)
   //               {
   //                  Agent targetAgent = MapUtil.Instance[updatePosition2 + direction];
   //                  if (targetAgent is EnergyLine)
   //                  {
   //                     (targetAgent as EnergyLine).UpdateLine();
   //                  }
   //                  if (targetAgent is HighWall)
   //                  {
   //                     (targetAgent as HighWall).UpdateWall();
   //                  }
   //               }
   //            }
   //            else
   //            {
   //               _selectedAgent.cellPosition = _previousSelectedBuildingCellPosition;
   //               MapUtil.RegisteAgent(_selectedAgent);

   //               _selectedAgent.transform.position = _previousSelectedBuildingPosition;
   //            }

   //            _selectedAgent = null;
   //         }
   //         break;
   //   }

   //   Shot3DUtil.currentCursorShotVisual
   //           .SetState(cursorShotState, 0.1f);
   //}



   //private void RemoveBuildingAction()
   //{
   //   CursorShotStateEnum cursorShotState = CursorShotStateEnum.CanBuild;
   //   Shot3DUtil.SetDrawingMesh(AgentType.None);


   //   if (Shot3DUtil.cursorCellType != AgentType.None)
   //   {
   //      IBuildingAgent agent = Shot3DUtil.GetAgentOnCurrentCursor() as IBuildingAgent;
   //      if (agent is not null)
   //      {
   //         cursorShotState = CursorShotStateEnum.CanBuild;
   //         if (Input.GetKeyDown(KeyCode.Space))
   //         {
   //            agent.Die();
   //         }
   //      }
   //      else
   //      {
   //         cursorShotState = CursorShotStateEnum.ImpossibleBuild;
   //      }
   //   }
   //   else
   //   {
   //      cursorShotState = CursorShotStateEnum.ImpossibleBuild;
   //   }

      

   //   Shot3DUtil.currentCursorShotVisual
   //           .SetState(cursorShotState, 0.1f);
   //}

   //private void AddBuildingAction()
   //{
   //   CursorShotStateEnum cursorShotState = CursorShotStateEnum.Default;
   //   AgentType currentBuildingType = uiManager.buildCanvas.CurrentBuildingType;
   //   Shot3DUtil.SetDrawingMesh(currentBuildingType);


   //   if (Shot3DUtil.cursorCellType == AgentType.None)
   //   {
   //      cursorShotState = CursorShotStateEnum.CanBuild;

   //      if (Input.GetKeyDown(KeyCode.Space))
   //      {
   //         switch (uiManager.buildCanvas.CurrentBuildingType)
   //         {
   //            case AgentType.ArrowTower:
   //               ArrowTowerSpawn();
   //               break;

   //            case AgentType.GoldStorage:
   //               GoldStorageSpawn();
   //               break;
   //            case AgentType.GoldMinor:
   //               GoldMinorSpawn();
   //               break;
   //            case AgentType.HighWall:
   //               HighWallSpawn();
   //               break;
   //            case AgentType.LowWall:
   //               LowWallSpawn();
   //               break;
   //            case AgentType.EnergyLine:
   //               EnergyLineSpawn();
   //               break;
   //         }

   //         Vector2Int addPosition = Shot3DUtil.cursorCellPosition;

   //         foreach (Vector2Int direction in MapHelper.FourDirection)
   //         {
   //            Agent targetAgent = MapUtil.Instance[addPosition + direction];
   //            if(targetAgent is EnergyLine)
   //            {
   //               (targetAgent as EnergyLine).UpdateLine();
   //            }
   //            if (targetAgent is HighWall)
   //            {
   //               (targetAgent as HighWall).UpdateWall();
   //            }
   //         }

   //      }
   //   }
   //   else
   //   {
   //      cursorShotState = CursorShotStateEnum.ImpossibleBuild;
   //   }

   //   Shot3DUtil.currentCursorShotVisual
   //         .SetState(cursorShotState, 0.1f);
   //}

   //private void LowWallSpawn()
   //{
   //   Vector3 spawnPosition = Shot3DUtil.DrawingMeshPosition;
   //   LowWall lowWall
   //      = BuildingUtil.Pop<LowWall>(Shot3DUtil.cursorCellPosition);

   //   MapUtil.RegisteAgent(lowWall);

   //   lowWall.WaitForWakeUp(() =>
   //   {
   //      Debug.Log("LowWall 持失");
   //      lowWall.transform.position = spawnPosition;
   //   });
   //}

   //private void HighWallSpawn()
   //{
   //   Vector3 spawnPosition = Shot3DUtil.DrawingMeshPosition;
   //   HighWall highWall
   //      = BuildingUtil.Pop<HighWall>(Shot3DUtil.cursorCellPosition);

   //   MapUtil.RegisteAgent(highWall);

   //   highWall.WaitForWakeUp(() =>
   //   {
   //      Debug.Log("HgihWall 持失");
   //      highWall.transform.position = spawnPosition;
   //      highWall.UpdateWall();
   //   });
   //}

   //private void GoldMinorSpawn()
   //{
   //   Vector3 spawnPosition = Shot3DUtil.DrawingMeshPosition;
   //   GoldMinor goldMinor
   //      = BuildingUtil.Pop<GoldMinor>(Shot3DUtil.cursorCellPosition);

   //   MapUtil.RegisteAgent(goldMinor);

   //   goldMinor.WaitForWakeUp(() =>
   //   {
   //      Debug.Log("GoldMinor 持失");
   //      goldMinor.transform.position = spawnPosition;
   //   });
   //}

   //private void GoldStorageSpawn()
   //{
   //   Vector3 spawnPosition = Shot3DUtil.DrawingMeshPosition;
   //   GoldStorage goldStorage
   //      = BuildingUtil.Pop<GoldStorage>(Shot3DUtil.cursorCellPosition);

   //   MapUtil.RegisteAgent(goldStorage);

   //   goldStorage.WaitForWakeUp(() =>
   //   {
   //      Debug.Log("GoldStorage 持失");
   //      goldStorage.transform.position = spawnPosition;
   //   });
   //}

   //private void EnergyLineSpawn()
   //{
   //   Vector3 spawnPosition = Shot3DUtil.DrawingMeshPosition;
   //   spawnPosition.y = 0.1f;
   //   EnergyLine energyLine
   //      = BuildingUtil.Pop<EnergyLine>(Shot3DUtil.cursorCellPosition);

   //   MapUtil.RegisteAgent(energyLine);

   //   energyLine.WaitForWakeUp(() =>
   //   {
   //      Debug.Log("EnergyLine 持失");
   //      energyLine.transform.position = spawnPosition;
   //      energyLine.UpdateLine();
   //   });
   //}

   //private static void ArrowTowerSpawn()
   //{
   //   Vector3 spawnPosition = Shot3DUtil.DrawingMeshPosition;
   //   ArrowTower arrowTower
   //      = BuildingUtil.Pop<ArrowTower>(Shot3DUtil.cursorCellPosition);

   //   MapUtil.RegisteAgent(arrowTower);

   //   arrowTower.WaitForWakeUp(() =>
   //   {
   //      Debug.Log("Arrow 持失");
   //      arrowTower.transform.position = spawnPosition;
   //   });
   //}

   //private void MoveAction()
   //{
   //   Vector2 moveDir = InputUtil.moveDirection;
   //   Vector3 newMovedir = new Vector3(moveDir.x, 0, moveDir.y);

   //   transform.position += newMovedir * (Time.deltaTime * _speed);
   //}

   //private void OnStateChange(GameMode previousState, GameMode currentState)
   //{
   //   bool isBuild = currentState == GameMode.Build;
   //   Shot3DUtil.isActive = isBuild;

   //   if(currentState == GameMode.View)
   //   {
   //      _virtualCam.Follow = transform;
   //      _virtualCam.LookAt = transform;
   //      _camTransposer.m_FollowOffset = _viewCamOffset;
   //      _camTransposer.m_XDamping = 0;
   //      _camTransposer.m_ZDamping = 0;
   //      uiManager.OnModeChange(GameMode.View);
   //   }
      
   //   if(previousState == GameMode.Upgrade)
   //   {
   //      _selectedUpgradeAgent.HideDebug();
   //      _selectedUpgradeAgent = null;
   //   }

   //   if(currentState == GameMode.Upgrade)
   //   {
   //      _selectedUpgradeAgent = Shot3DUtil.GetAgentOnCurrentCursor() as IBuildingAgent;
   //      _virtualCam.Follow = (_selectedUpgradeAgent as MonoBehaviour).transform;
   //      _virtualCam.LookAt = (_selectedUpgradeAgent as MonoBehaviour).transform;
   //      _camTransposer.m_FollowOffset = _UpgradeCamOffset;
   //      _camTransposer.m_XDamping = onUpgradeDamping.x;
   //      _camTransposer.m_ZDamping = onUpgradeDamping.y;
   //      Shot3DUtil.SetCursorShotVisual(ToolBarEnum.Upgrade);


   //      _selectedUpgradeAgent.ShowDebug();
   //      uiManager.OnModeChange(GameMode.Upgrade);
   //   }
   //}
}
