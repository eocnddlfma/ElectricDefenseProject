using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MoveBuildingOrder
{
   None,
   Grab,
   Stay,
}


[System.Serializable]
public class PlayerBuildInfo
{

   public event Action<AgentType, Vector2Int> OnAddEvent = null;
   public event Action<Vector2Int> OnEraseEvent = null;
   public event Action OnMoveStartEvent = null;
   public event Action OnMoveDuringEvent = null;
   public event Action OnMoveEndEvent = null;


   private Agent _owner;
   private bool isAlreadyClick = false;
  
   public void Initalize(Agent owner)
   {
      _owner = owner;
   }


   public void MoveBuildingAction()
   {
      CursorShotStateEnum cursorShotState = CursorShotStateEnum.Default;
      Shot3DUtil.SetDrawingMesh(AgentType.None);


      if (InputUtil.isClick && !isAlreadyClick) // Click Enter
      {
         cursorShotState = CursorShotStateEnum.CanBuild;
         OnMoveStartEvent?.Invoke();
         isAlreadyClick = true;
      }
      else if(InputUtil.isClick) // Click Stay
      {
         if (Shot3DUtil.cursorCellType == AgentType.None)
         {
            cursorShotState = CursorShotStateEnum.CanBuild;
         }
         else
         {
            cursorShotState = CursorShotStateEnum.ImpossibleBuild;
         }
         OnMoveDuringEvent?.Invoke();
      }
      else if(!InputUtil.isClick && isAlreadyClick) // Click Exit
      {
         OnMoveEndEvent?.Invoke();
         isAlreadyClick = false;
      }
         

      Shot3DUtil.currentCursorShotVisual
              .SetState(cursorShotState, 0.1f);
   }



   public void RemoveBuildingAction()
   {
      CursorShotStateEnum cursorShotState = CursorShotStateEnum.CanBuild;
      Shot3DUtil.SetDrawingMesh(AgentType.None);


      if (Shot3DUtil.cursorCellType != AgentType.None
         && Shot3DUtil.GetAgentOnCurrentCursor() is not null)
      {
         if (Input.GetKeyDown(KeyCode.Space))
         {
            OnEraseEvent?.Invoke(Shot3DUtil.cursorCellPosition);
         }
         cursorShotState = CursorShotStateEnum.CanBuild;
      }
      else
      {
         cursorShotState = CursorShotStateEnum.ImpossibleBuild;
      }



      Shot3DUtil.currentCursorShotVisual
              .SetState(cursorShotState, 0.1f);
   }


   public void AddBuildingAction()
   {
      CursorShotStateEnum cursorShotState = CursorShotStateEnum.Default;
      AgentType currentBuildingType = UIUtil.Instance.buildCanvas.CurrentBuildingType;
      Shot3DUtil.SetDrawingMesh(currentBuildingType);


      if (Shot3DUtil.cursorCellType == AgentType.None)
      {
         cursorShotState = CursorShotStateEnum.CanBuild;

         if (Input.GetKeyDown(KeyCode.Space))
         {
            Vector2Int addPosition = Shot3DUtil.cursorCellPosition;
            OnAddEvent?.Invoke
               (UIUtil.Instance.buildCanvas.CurrentBuildingType, addPosition);

         }
      }
      else
      {
         cursorShotState = CursorShotStateEnum.ImpossibleBuild;
      }

      Shot3DUtil.currentCursorShotVisual
            .SetState(cursorShotState, 0.1f);
   }


}
