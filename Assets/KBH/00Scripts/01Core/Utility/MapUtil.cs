using System;
using DG.Tweening;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapUtil : MonoSingleton<MapUtil>
{
   [Header("Map Settings")]
   [SerializeField] private Vector2Int _mapArea;
   [SerializeField] private float _scale;
   [SerializeField] private float _height = 2f;
   [SerializeField] private int _seed;
   [SerializeField] [Range(0, 1)] private float _rockSpawnFrequency;
   [SerializeField] [Range(0, 1)] private float _goldRate;

   [Header("Mesh Combine")]
   [SerializeField] private float _cellScale = 1f;
   [SerializeField] private Mesh _blockMesh;
   [SerializeField] private MeshFilter _goldMeshCombiner;
   [SerializeField] private MeshFilter _rockMeshCombiner;
   
   [Header("Map Data")]
   [SerializeField] private AgentType[,] _mapInfo;
   public AgentType this[int x, int y]
   {
      get
      {
         if(x < _mapArea.x && x >= 0
            && y < _mapArea.y && y >= 0)
         {
            if(agentRegisteDictionary
               .TryGetValue(new Vector2Int(x, y), out Agent result))
            {
               return result.agentType;
            }
            else
            {
               return _mapInfo[x, y];
            }
         }
         else
         {
            return AgentType.None;
         }
      }
   }

   public Agent this[Vector2Int position]
   {
      get
      {
         if (position.x < _mapArea.x && position.x >= 0
            && position.y < _mapArea.y && position.y >= 0)
         {
            if (agentRegisteDictionary
               .TryGetValue(position, out Agent result))
            {
               return result;
            }
            else
            {
               return null;
            }
         }
         return null;
      }
   }

   [Header("Block Mesh Visual")]
   [SerializeField] private MeshRenderer _goldMeshRenderer;
   [SerializeField] private MeshCollider _goldMeshCollider;

   [SerializeField] private MeshRenderer _rockMeshRenderer;
   [SerializeField] private MeshCollider _rockMeshCollider;

   private readonly int _percentHash = Shader.PropertyToID("_Percent");

   [Header("Bottom Mesh Visual")]
   [SerializeField] private MeshRenderer _bottomMeshRenderer;
   [SerializeField] private float _bottomStroke = 0.05f;
   private readonly int _centerHash = Shader.PropertyToID("_Center");
   private readonly int _UnitHash = Shader.PropertyToID("_Unit");
   private readonly int _StrokeHash = Shader.PropertyToID("_Stroke");

   public Dictionary<Vector2Int, Agent> agentRegisteDictionary
      = new Dictionary<Vector2Int, Agent>();


#if UNITY_EDITOR

   [Header("Debugger")]
   [SerializeField] private Vector2Int position;

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawCube(CellToWorld(position), new Vector3(1, 10, 1));
   }

#endif

   private void Awake()
   {
      GenerateMap();
      SetMaterial(isShow : true, 2f);
   }


   private void GenerateMap()
   {
      _mapInfo = new AgentType[_mapArea.x, _mapArea.y];
      List<CombineInstance> goldCombineList = new List<CombineInstance>();
      List<CombineInstance> rockCombineList = new List<CombineInstance>();

      Vector2 center = (Vector2)(_mapArea / 2) * _cellScale;
      _bottomMeshRenderer.material.SetVector(_centerHash, center);
      _bottomMeshRenderer.material.SetVector(_UnitHash, Vector2.one * _cellScale);
      _bottomMeshRenderer.material.SetFloat(_StrokeHash, _bottomStroke);

      for (int i = 0; i < _mapArea.y; ++i)
      {
         for (int j = 0; j < _mapArea.x; ++j)
         {
            float xAmount = j * _cellScale;
            float yAmount = i * _cellScale;

            float perlin = Mathf.PerlinNoise(j / (float)_mapArea.x * _scale + _seed, i / (float)_mapArea.y * _scale + _seed);

            if (perlin >= _rockSpawnFrequency)
            {
               _mapInfo[j, i] = AgentType.None;
               //Debug.Log($"({j},{i}) : {_mapInfo[j, i]} ");
               continue;
            }

            bool isRock = true;
            float result = perlin;


            if (_goldRate >= perlin * (1 / _rockSpawnFrequency))
            {
               result = perlin - _goldRate;
               isRock = false;
            }

            result = result * _height * (1f / _rockSpawnFrequency);



            Matrix4x4 meshTrm = Matrix4x4.TRS(
               new Vector3(xAmount - center.x, 0, yAmount - center.y),  // Position
               Quaternion.identity,  // Rotation
               new Vector3(1, result, 1) * _cellScale); // Scale

            Mesh copiedMesh = Instantiate(_blockMesh);

            CombineInstance targetPrefabCombine = new CombineInstance();
            targetPrefabCombine.mesh = copiedMesh;
            targetPrefabCombine.transform = meshTrm;

            if (isRock)
            {
               rockCombineList.Add(targetPrefabCombine);
               _mapInfo[j, i] = AgentType.Rock;
               //Debug.Log($"({j},{i}) : {_mapInfo[j, i]} ");
            }
            else
            {
               goldCombineList.Add(targetPrefabCombine);
               _mapInfo[j, i] = AgentType.Gold;
               //Debug.Log($"({j},{i}) : {_mapInfo[j, i]} ");
            }

         }
      }


      var rockMesh = new Mesh();
      rockMesh.CombineMeshes(rockCombineList.ToArray()); 
      _rockMeshCombiner.mesh = rockMesh;
      _rockMeshCollider.sharedMesh = rockMesh;

      var goldMesh = new Mesh();
      goldMesh.CombineMeshes(goldCombineList.ToArray());
      _goldMeshCombiner.mesh = goldMesh;
      _goldMeshCollider.sharedMesh = goldMesh;

      
   }

   private NavMeshSurface surface;

   public static Vector2Int WorldToCell(Vector3 position)
   {
      Vector2Int cellPos
         =  new Vector2Int(
            (int)((position.x + ((float)Instance._mapArea.x/2)) / Instance._cellScale), 
            (int)((position.z + ((float)Instance._mapArea.y/2)) / Instance._cellScale));
      return cellPos;
   }

   public static Vector3 CellToWorld(Vector2Int cellPos)
   {
      Vector3 result = new Vector3(cellPos.x, 0, cellPos.y);
      Vector3 center = new Vector3(Instance._mapArea.x / 2, 0, Instance._mapArea.y / 2);
      result -= center;

      return result;
   }


   public static void SetMaterial(bool isShow, float time)
   {
      if (isShow)
      {
         Instance._goldMeshRenderer.material.DOFloat(0, Instance._percentHash, time);
         Instance._rockMeshRenderer.material.DOFloat(0, Instance._percentHash, time);
      }
      else
      {
         Instance._goldMeshRenderer.material.DOFloat(1, Instance._percentHash, time);
         Instance._rockMeshRenderer.material.DOFloat(1, Instance._percentHash, time);
      }
   }

   
   public static AgentType WorldToCellType(Vector3 position)
   {
      Vector2Int cellPos = WorldToCell(position);
      return Instance._mapInfo[cellPos.x, cellPos.y];
   }

   public static void RegisteAgent(Agent agent)
   {
      if(!Instance.agentRegisteDictionary.ContainsKey(agent.cellPosition))
      {
         Instance.agentRegisteDictionary[agent.cellPosition] = agent;
      }
   }

   public static void RemoveAgent(Agent agent)
   {
      if(Instance.agentRegisteDictionary.ContainsKey(agent.cellPosition))
      {
         Instance.agentRegisteDictionary.Remove(agent.cellPosition);
      }
   }
}

public static class MapHelper
{
   public readonly static Vector2Int[] FourDirection = new Vector2Int[4]
   {
      Vector2Int.right,
      Vector2Int.left,
      Vector2Int.up,
      Vector2Int.down
   };
}