using System.Collections.Generic;
using UnityEngine;

public class MapUtil : MonoSingleton<MapUtil>
{
   [System.Serializable]
   struct AgentPrefabInfo
   {
      public AgentType agentType;
      public Agent agentPrefab;
   }

   [SerializeField] private List<AgentPrefabInfo> _agentPrefabList;
   private Dictionary<AgentType, Agent> _agentPrefabDictionary;


   private Dictionary<AgentType, Stack<Agent>> _agentStacks;

   private void Awake()
   {
      _agentPrefabDictionary = new Dictionary<AgentType, Agent>();
      _agentStacks = new Dictionary<AgentType, Stack<Agent>>();
      for (int i = 0; i < _agentPrefabList.Count; ++i)
      {
         _agentPrefabDictionary[_agentPrefabList[i].agentType]
            = _agentPrefabList[i].agentPrefab;

         _agentStacks[_agentPrefabList[i].agentType] = new Stack<Agent>();
      }
   }



   public static Agent PopBuilding(AgentType type)
   {
      Agent target = Instantiate(Instance._agentPrefabDictionary[type], Instance.transform);
      return target;
   }



}
