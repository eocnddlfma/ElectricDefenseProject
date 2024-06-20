using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class DecideUIStayTag : MonoTag<bool>, IState
{
   [SerializeField] private DecideUIReference _reference;
   [SerializeField] private float _valueChangeSpeed = 2f;
   [SerializeField] private float _percentChangeSpeed = 0.1f;
   
   [Header("Visual")]
   [SerializeField] private SelectDummyUI _selectVisual;

   [Header("Debug")]
   [SerializeField] private int _currentBlockIdx = 0;
   [SerializeField] private bool isWait = false;
   [SerializeField] private bool isFirstUpdate = true;

   // 여기서는 다 셋팅되어 있는 UI를 이용해서
   // UI 선택, 값 조절 및 저장과 같은 제일 주된 일들을 
   // 처리해줘야 한다. 

   public override void Initialize()
   {
      Current = true;
      isFirstUpdate = true;
   }

   public void OnEnter()
   {
      _currentBlockIdx = 0;
      _selectVisual.SetTrm(
         _reference.currentOpenedBlock.childs[0].visualTrm, 
         0.2f);
   }


   public override void BaseUpdate()
   {
      if (isFirstUpdate)
      {
         OnEnter();
         isFirstUpdate = false;
      }

      if (isWait) return;


      InputData input = _reference.currentInput;


      DecideUIBlock currentBlock = null;
      bool hasChildList = _reference.currentOpenedBlock.childs is not null;
      bool hasChild = _reference.currentOpenedBlock.childCount > 0;
      if (hasChildList && hasChild)
      {
         currentBlock = _reference.currentOpenedBlock[_currentBlockIdx];
      }


      if(Mathf.Abs(input.arrowDir) > 0)
         ChangeBlockValue(input.arrowDir, currentBlock);
      
      if (Mathf.Abs(input.nextSelectDir) > 0)
         SelectMove(input.nextSelectDir, currentBlock);


      if(Input.GetKeyDown(KeyCode.Return))
         InterectButton(currentBlock);
      
   }


   public void OnExit()
   {
      _selectVisual.SetScaleZero(0.2f);
      isFirstUpdate = true;
      Current = false;
   }

   private void ChangeBlockValue(float arrowDir, DecideUIBlock currentBlock)
   {
      if(currentBlock is ValueUIBlock)
      {
         ValueUIBlock valueBlock = currentBlock as ValueUIBlock;

         switch (valueBlock.valueType)
         {
            case UIValueTypeEnum.percent:
               valueBlock.Percent += _percentChangeSpeed * Time.deltaTime * arrowDir;
               break;


            case UIValueTypeEnum.value:
               valueBlock.Value += _valueChangeSpeed * Time.deltaTime * arrowDir ;
               break;
         }
      }
   }

   private void SelectMove(float selectDir, DecideUIBlock currentBlock)
   {
      if (currentBlock.childs is null) return;

      int nextIdx = (int)(selectDir + _currentBlockIdx);
      bool CanMoveNext
         = (nextIdx < _reference.currentOpenedBlock.childs.Count)
               && (nextIdx >= 0);

      if (CanMoveNext)
      {
         Debug.Log(nextIdx);
         _currentBlockIdx = nextIdx;

         RectTransform currentBlockRectTrm
            = _reference.currentOpenedBlock[_currentBlockIdx].visualTrm;

         _selectVisual.SetTrm(currentBlockRectTrm, 0.2f);

         isWait = true;
         DOVirtual.DelayedCall(0.3f, () => isWait = false);
      }
   }

   private void InterectButton(DecideUIBlock currentBlock)
   {
      if (currentBlock is EnterUIBlock)
      {
         _reference.previousOpenedBlock
            = _reference.currentOpenedBlock;

         _reference.currentOpenedBlock
            = currentBlock;

         OnExit();
      }
      else if (currentBlock is ExitUIBlock)
      {
         _reference.previousOpenedBlock
            = _reference.currentOpenedBlock;

         if (_reference.currentOpenedBlock.parentBlock is not null)
         {
            _reference.currentOpenedBlock
               = _reference.currentOpenedBlock.parentBlock;
         }

         OnExit();
      }
      else if (currentBlock is KeyReadUIBlock)
      {
         isWait = true;
      }
   }

   
}
