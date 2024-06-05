using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUtil : MonoSingleton<BuildingUtil>
{
   [System.Serializable]
   struct BuildingPoolData
   {
      public int marginCount;
      public Agent buildingPrefab;
      public Transform parent;
   }

   [SerializeField] private BuildingPoolData[] _poolDataList;
   private Dictionary<AgentType, Stack<Agent>> _agentPoolDictionary;
   private Dictionary<AgentType, BuildingPoolData> _agentPoolDataDictionary;
   private Dictionary<System.Type, AgentType> _agentTypeDictionary;

   private void Awake()
   {
      _agentPoolDictionary = new Dictionary<AgentType, Stack<Agent>>();
      _agentPoolDataDictionary = new Dictionary<AgentType, BuildingPoolData>();
      _agentTypeDictionary = new Dictionary<System.Type, AgentType>();

      for(int i = 0; i<_poolDataList.Length; ++i)
      {
         AgentType poolType = _poolDataList[i].buildingPrefab.agentType;
         
         
         Transform targetPoolTypeParent = new GameObject($"||Pool|| {poolType.ToString()}\t||").transform;
         Instance._poolDataList[i].parent = targetPoolTypeParent;

         targetPoolTypeParent.SetParent(transform);
         _agentPoolDictionary.Add(poolType, new Stack<Agent>());
         _agentPoolDataDictionary.Add(poolType, _poolDataList[i]);

         _agentTypeDictionary[_poolDataList[i].buildingPrefab.GetType()] = poolType;

         for(int j = 0; j< _poolDataList[i].marginCount; ++j)
         {
            Agent agent
               = Instantiate(_poolDataList[i].buildingPrefab, targetPoolTypeParent);
            agent.gameObject.SetActive(false);
            _agentPoolDictionary[poolType].Push(agent);
         }
      }
   }


   public static T Pop<T>(Vector2Int registePosition) where T : Agent
   {
      System.Type popType = typeof(T);
      AgentType popTypeEnum = Instance._agentTypeDictionary[popType];
      Stack<Agent> targetStack
         = Instance._agentPoolDictionary[popTypeEnum];


      if (targetStack.Count <= 0)
      {
         BuildingPoolData poolData = Instance._agentPoolDataDictionary[popTypeEnum];
         for(int i = 0; i<poolData.marginCount; ++i)
         {
            Agent agent
               = Instantiate(poolData.buildingPrefab, poolData.parent);
            agent.gameObject.SetActive(false);
            targetStack.Push(agent);
         }
      }

      T result = targetStack.Pop() as T;
      result.cellPosition = registePosition;

      return result;
   }

   public static void Push(Agent agent)
   {

      AgentType pushType = agent.agentType;

      agent.gameObject.SetActive(false);
      Instance._agentPoolDictionary[pushType].Push(agent);
   }


}


public static class BuildingUtilHelper
{

   public static void WaitForWakeUp(this Agent agent, System.Action Callback)
   {
      if (!agent) return;
      if (agent.gameObject.activeSelf) return;

      agent.gameObject.SetActive(true);
      if (BuildingUtil.Instance is null)
      {
         Debug.LogError("Building WakeUp시킬 수 없습니다. BuildingUtil가 존재하지 않습니다. ");
         return;
      }
      BuildingUtil.Instance.StartCoroutine(WakeUpRoutine(agent, Callback));
   }

   private static IEnumerator WakeUpRoutine(Agent agent, System.Action Callback)
   {
      yield return new WaitForEndOfFrame();
      Callback?.Invoke();
      agent?.WakeUpAction();
   }


}
