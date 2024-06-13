using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OperateState
{
   Sleep,
   Run
}


[System.Serializable]
public class OperateAgent<T>
   where T : Enum
{
   protected MonoBehaviour _owner;

   [SerializeField] protected OperateState _operateState;
   public OperateState OperateState => _operateState;
   public ILinkable<T> _parent;

   [SerializeField] private uint _lastCount = 0;
   public int LastCount => (int)_lastCount;
   [SerializeField] private bool _isLoop = false;
   public bool IsLoop => _isLoop;

   private YieldInstruction _yieldInstruction;
   private Coroutine _runRoutine;

   public virtual void Initialize(MonoBehaviour owner, YieldInstruction yieldInstruction, ILinkable<T> parent = null)
   {
      _owner = owner;
      _yieldInstruction = yieldInstruction;
      _parent = parent;

      _operateState = OperateState.Sleep;
   }

   public virtual void Run()
   {
      if (_operateState == OperateState.Run)
         return;

      if(_runRoutine is not null)
      {
         _owner.StopCoroutine(_runRoutine);
      }
      _operateState = OperateState.Run;
      _runRoutine = _owner.StartCoroutine(AutoUpdateRoutine());
   }


   public virtual void Sleep()
   {
      if (_operateState == OperateState.Sleep)
         return;

      if(_runRoutine is not null)
      {
         _owner.StopCoroutine(_runRoutine);
      }
      _operateState = OperateState.Sleep;
   }

   /*Enable*/
   public void Enable(int times = 1)
   {
      if(times > 0)
      {
         _lastCount = (uint)Mathf.Max(_lastCount + times, 0);
      }
      else if(times == -1)
      {
         _isLoop = true;
      }
   }
   
   /*Disable*/
   public void Disable()
   {
      _lastCount = 0;
      _isLoop = false;
   }
   public void StopLoop()
   {
      _isLoop = false;
   }

   /*Update*/
   public virtual void Update()
   {
      if (!_isLoop)
      {
         if (_lastCount <= 0) return;
         --_lastCount;
      }
   }
   public IEnumerator AutoUpdateRoutine()
   {
      while (true)
      {
         Update();
         yield return _yieldInstruction;
      }
   }


}
