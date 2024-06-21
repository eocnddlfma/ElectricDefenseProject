using UnityEngine;

[System.Serializable]
public class Player : OperateAgent<AgentType>
{

   [Header("Compoents")]
   public Crystal mainCrystal;
   public PlayerMoveInfo moveInfo;
   public StateMachine<GameMode> stateMachine;
   public PlayerCamInfo camInfo;
   public PlayerBuildInfo buildInfo;
   public PlayerUpgradeInfo upgradeInfo;


   private GameMode _previousState;
   public Vector2 MoveDir => InputUtil.moveDirection;


   public override void Initialize(MonoBehaviour owner, YieldInstruction yieldInstruction, ILinkable<AgentType> parent = null)
   {
      base.Initialize(owner, yieldInstruction, parent);

      Time.timeScale = 1;

      Agent agentOwner = owner as Agent;
      stateMachine.Intialize(agentOwner, GameMode.View);
      moveInfo.Initialize(agentOwner);
      camInfo.Initialize(agentOwner);
      buildInfo.Initalize(agentOwner);
   }



   public override void Run()
   {
      base.Run();
   }

   public override void Update()
   {
      base.Update();
      stateMachine.Update();

      if (_previousState != stateMachine.State)
      {
         OnStateChange(_previousState, stateMachine.State);
      }
      UpdateGameMode(stateMachine.State);

      _previousState = stateMachine.State;
   }


   public override void Sleep()
   {
      base.Sleep();
   }


   private void UpdateGameMode(GameMode currentState)
   {
      switch (currentState)
      {
         case GameMode.View:
            Shot3DUtil.SetCursorShotVisual(ToolBarEnum.None);
            moveInfo.Move(MoveDir);
            break;

         case GameMode.Build:
            BuildAction();
            moveInfo.Move(MoveDir);
            break;

         case GameMode.Upgrade:
            UpgradeAction();
            break;

         case GameMode.Shop:
            break;

      }

   }

   private void UpgradeAction()
   {
   }

   private void BuildAction()
   {
      ToolBarEnum currentToolbarType
         = UIUtil.Instance.buildCanvas.CurrentToolbarType;
      Shot3DUtil
         .SetCursorShotVisual(currentToolbarType);

      switch (currentToolbarType)
      {
         case ToolBarEnum.None:
            break;

         case ToolBarEnum.Add:
            buildInfo.AddBuildingAction();
            break;

         case ToolBarEnum.Remove:
            buildInfo.RemoveBuildingAction();
            break;

         case ToolBarEnum.Move:
            buildInfo.MoveBuildingAction();
            break;
      }

      bool isAdd = currentToolbarType == ToolBarEnum.Add;
      UIUtil.Instance.buildCanvas.IsBuildingBarActive = isAdd;
   }




   private void OnStateChange(GameMode previousState, GameMode currentState)
   {
      bool isBuild = currentState == GameMode.Build;
      Shot3DUtil.isActive = isBuild;

      if (currentState == GameMode.View)
      {
         UIUtil.Instance.OnModeChange(GameMode.View);
      }

      if (previousState == GameMode.Upgrade)
      {
         // Upgrade 해제
         upgradeInfo.EndUpgradeEvent?.Invoke();
         camInfo.SetCameraSetting(GameMode.View, _owner.transform);
      }

      if (currentState == GameMode.Upgrade)
      {
         // Upgrade 제작
         Transform target = upgradeInfo.UpgradeEvent?.Invoke(Shot3DUtil.cursorCellPosition);
         if (target)
         {
            camInfo.SetCameraSetting(GameMode.Upgrade, target);
            Shot3DUtil.SetCursorShotVisual(ToolBarEnum.Upgrade);
            UIUtil.Instance.OnModeChange(GameMode.Upgrade);
         }
      }
   }


}
