using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILinkable<R>
   where R : Enum
{
   public void AddAgentAt(OperateAgent<R> agent, params R[] types);
   public void RemoveAgentAt(OperateAgent<R> agent, params R[] types);
   public void AddAgentGlobal(OperateAgent<R> agent);
   public void RemoveAgentGlobal(OperateAgent<R> agent);
   public void EnableMessage(R type, int count = 1);
   public void DisableMessage(R type);
   public void StopLoopMessage(R type);
}

[System.Serializable]
public class LinkAgent<T, R> : OperateAgent<T>, ILinkable<R>
   where T : Enum
   where R : Enum
{
   public Dictionary<R, List<OperateAgent<R>>> _operateAgentListDictionary
      = new Dictionary<R, List<OperateAgent<R>>>();

   public override void Initialize(MonoBehaviour owner, YieldInstruction yieldInstruction, ILinkable<T> parent = null)
   {
      R[] enumList = Enum.GetValues(typeof(R)) as R[];
      
      foreach(R element in enumList)
      {
         _operateAgentListDictionary
            .Add(element, new List<OperateAgent<R>>());
      }

      base.Initialize(owner, yieldInstruction, parent);
   }

   public void AddAgentAt(OperateAgent<R> agent, params R[] types)
   {
      for(int i = 0; i<types.Length; ++i)
      {
         _operateAgentListDictionary[types[i]].Add(agent);
      }
   }

   public void RemoveAgentAt(OperateAgent<R> agent, params R[] types)
   {
      for (int i = 0; i < types.Length; ++i)
      {
         _operateAgentListDictionary[types[i]].Remove(agent);
      }
   }

   public void AddAgentGlobal(OperateAgent<R> agent)
   {
      foreach(var agentListPair in _operateAgentListDictionary)
      {
         agentListPair.Value.Add(agent);
      }
   }

   public void RemoveAgentGlobal(OperateAgent<R> agent)
   {
      foreach (var agentListPair in _operateAgentListDictionary)
      {
         agentListPair.Value.Remove(agent);
      }
   }

   public void EnableMessage(R type, int count = 1)
   {
      List<OperateAgent<R>> agentList = _operateAgentListDictionary[type];

      for(int i = 0; i<agentList.Count; ++i)
      {
         agentList[i].Enable(count);
      }
   }

   public void DisableMessage(R type)
   {
      List<OperateAgent<R>> agentList = _operateAgentListDictionary[type];

      for (int i = 0; i < agentList.Count; ++i)
      {
         agentList[i].Disable();
      }
   }

   public void StopLoopMessage(R type)
   {
      List<OperateAgent<R>> agentList = _operateAgentListDictionary[type];

      for (int i = 0; i < agentList.Count; ++i)
      {
         agentList[i].StopLoop();
      }
   }

   
}
