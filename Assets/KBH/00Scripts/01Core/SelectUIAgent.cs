//using DG.Tweening;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static UnityEngine.RectTransform;

//public enum PanelUIType 
//{
//   Menu,
//   Game,
//   Exit
//}



//public enum SelectableAgentType
//{
//   None,
//   Button,
//   Gauge,
//   Value,
//   Text,
//   InputField,
//   JoinCode,
//   KeyRead,
//   Exit
//}

//[System.Serializable]
//public class SelectUIAgent : LinkAgent<PanelUIType, SelectableAgentType>
//{
//   [Header("Base Information")]
//   [SerializeField] private PanelUIType _type;
//   [SerializeField] private SelectUIReference _reference;
//   [SerializeField] private SelectInput _selectInput;


//   [Header("Currnet Node Debug")]
//   public List<SelectableUIBase> selectUIBaseList;

//   public SelectUINode currentNode;
//   public int currentSelectIdx = 0;
//   [Space(25)]

//   [Header("RenderSetting")]
//   public SelectDummyUI selectVisualizer;
//   public float renderGap = 10f;
//   public float transitionTime = 2f;
//   public bool isTransition = false;
   


//   public void AdditionalSetting(PanelUIType type, SelectUIReference reference)
//   {
//      selectUIBaseList = new List<SelectableUIBase>();
//      _reference = reference;
//      _type = type;
//   }

//   public override void Run()
//   {
//      base.Run();
//      ConnectWithNodeList(null);
//      selectVisualizer.SetVisible(true);

//      _selectInput.OnEnterEvent += MoveToInnerTransition;
//      _selectInput.OnArrowValueEvent += SetCurrentSelection;
//      _selectInput.OnNextSelectEvent += ChangeToNextSelection;
//   }

//   private void ChangeToNextSelection(int inp)
//   {
//      if ((currentSelectIdx + inp) < 0
//         || (currentSelectIdx + inp) >= selectUIBaseList.Count)
//         return;

//      currentSelectIdx += inp;

//      selectVisualizer.SetScale
//         (selectUIBaseList[currentSelectIdx].RectTrm.sizeDelta, transitionTime / 2);

//      Vector3 reducingPosition = selectUIBaseList[currentSelectIdx].RectTrm.localPosition;
//      for(int i = 0; i<selectUIBaseList.Count; ++i)
//      {
//         selectUIBaseList[i].RectTrm
//            .DOLocalMove(selectUIBaseList[i].RectTrm.localPosition - reducingPosition,
//            transitionTime/2);
//      }
//   }

//   private void SetCurrentSelection(int inp)
//   {

//   }

//   private void MoveToInnerTransition()
//   {

//   }

//   public override void Sleep()
//   {
//      base.Sleep();
//   }

//   private void DisconnectWithNodeList()
//   {
//      foreach(var uiBase in selectUIBaseList)
//      {
//         uiBase.UnSubscribe(this);
//         DOVirtual.DelayedCall(transitionTime / 2
//            , () => GameObject.Destroy(uiBase.gameObject));
//      }
//   }
//   private void ConnectWithNodeList(SelectUINode nodeRoot)
//   {
//      currentNode = nodeRoot ?? _reference.selectUITreeInfo.tree;

//      bool isVertical = currentNode.isVerticalDraw;
//      _selectInput.Initialize(isVertical, transitionTime);

//      RectTransform renderTrm = _reference.selectUIRenderTrm;

//      if(currentNode.list == null)
//      {
//         Debug.LogError("Connecting CurrentNode List is null. ");
//         return;
//      }


//      Vector2 accumulatedSize = Vector3.zero;
//      for (int i = 0; i < currentNode.list.Count; ++i)
//      {
//         SelectableUIBase selectableUIPrefab
//            = _reference.selectUIPrefabList[currentNode.list[i].agentEventType];

//         // 나중에 Pool로 바꿔줍니다. 
//         selectableUIPrefab = GameObject.Instantiate(selectableUIPrefab);
//         selectableUIPrefab.Initalize(this, isVertical, i, transitionTime/2);

//         // 여기서 모양과 위치를 잡아주면 된다. 
//         SelectUIRender(i, accumulatedSize, renderTrm, isVertical, selectableUIPrefab);
//         selectUIBaseList.Add(selectableUIPrefab);

//         accumulatedSize += selectableUIPrefab.RectTrm.sizeDelta;
//      }

//      selectVisualizer.SetScale(
//         (Vector3)selectUIBaseList[0].RectTrm.sizeDelta + Vector3.forward
//         , transitionTime);
//   }

//   private void SelectUIRender(int idx, Vector3 accumulatedSize, 
//      RectTransform renderTrm, bool isVertical, SelectableUIBase drawingTarget)
//   {
//      // 여기서 그려주는 작업을 해줄 것입니다. 
//      drawingTarget.RectTrm.SetParent(renderTrm);
//      drawingTarget.PaintColor(transitionTime / 2);

//      if (isVertical)
//      {
//         drawingTarget.RectTrm.localPosition
//             = accumulatedSize * Vector2.down
//             + renderGap * idx * Vector2.down;
//      }
//      else
//      {
//         drawingTarget.RectTrm.localPosition
//             = accumulatedSize * Vector2.right
//             + renderGap * idx * Vector2.right;
//      }
//   }
    
//   public void TransitionToTargetIdx(int idx)
//   {
//      SelectUINode nextNode = currentNode.list[idx];
//      DisconnectWithNodeList();
//      DOVirtual.DelayedCall(transitionTime/2, () => ConnectWithNodeList(nextNode));
//   }

//   public void ExitCurrentList()
//   {
//      SelectUINode nextNode = currentNode.parent;
//      DisconnectWithNodeList();

//      if (nextNode is not null)
//         DOVirtual.DelayedCall(transitionTime / 2, () => ConnectWithNodeList(nextNode));
//      else
//         Sleep();
//   }


//   public override void Update()
//   {
//      base.Update();
//      _selectInput.UpdateInput();
//   }

//}
