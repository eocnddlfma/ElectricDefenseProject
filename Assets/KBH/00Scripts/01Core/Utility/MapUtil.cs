using System.Collections.Generic;
using UnityEngine;

public class MapUtil : MonoSingleton<MapUtil>
{
   

   [Header("Map Settings")]
   [SerializeField] private Vector2 center;
   [SerializeField] private Vector2Int mapArea;
   [SerializeField] private float scale;
   [SerializeField] private float height = 2f;
   [SerializeField] private int seed;
   [SerializeField] [Range(0, 1)] private float RockSpawnFrequency;
   [SerializeField] [Range(0, 1)] private float GoldRate;

   [Header("Mesh Combine")]
   [SerializeField] private float cellScale = 1f;
   [SerializeField] private Mesh _blockMesh;
   [SerializeField] private MeshFilter _goldMeshCombiner;
   [SerializeField] private MeshFilter _rockMeshCombiner;

   [Header("Map Data")]
   [SerializeField] private MapBlockType[,] mapInfo;

   private void Awake()
   {
      GenerateMap();
   }

   private void GenerateMap()
   {
      mapInfo = new MapBlockType[mapArea.x, mapArea.y];
      List<CombineInstance> goldCombineList = new List<CombineInstance>();
      List<CombineInstance> rockCombineList = new List<CombineInstance>();

      for (int i = 0; i < mapArea.y; ++i)
      {
         for (int j = 0; j < mapArea.x; ++j)
         {
            float perlin = Mathf.PerlinNoise(j / (float)mapArea.x * scale + seed, i / (float)mapArea.y * scale + seed);

            Debug.Log(WorldToCell(new Vector3(j - center.x, 0, i - center.y)));
            if (perlin >= RockSpawnFrequency)
            {
               mapInfo[j, i] = MapBlockType.None;
               continue;
            }

            bool isRock = true;
            float result = perlin;


            if (GoldRate >= perlin * (1 / RockSpawnFrequency))
            {
               result = perlin - GoldRate;
               isRock = false;
            }

            result = result * height * (1f / RockSpawnFrequency);



            Matrix4x4 meshTrm = Matrix4x4.TRS(new Vector3(j - center.x, 0, i - center.y), Quaternion.identity, new Vector3(1, result, 1) * cellScale);
            Mesh copiedMesh = Instantiate(_blockMesh);

            CombineInstance targetPrefabCombine = new CombineInstance();
            targetPrefabCombine.mesh = copiedMesh;
            targetPrefabCombine.transform = meshTrm;

            if (isRock)
            {
               rockCombineList.Add(targetPrefabCombine);
               mapInfo[j, i] = MapBlockType.Rock;
            }
            else
            {
               goldCombineList.Add(targetPrefabCombine);
               mapInfo[j, i] = MapBlockType.Gold;
            }

         }
      }


      var rockMesh = new Mesh();
      rockMesh.CombineMeshes(rockCombineList.ToArray());
      _rockMeshCombiner.mesh = rockMesh;

      var goldMesh = new Mesh();
      goldMesh.CombineMeshes(goldCombineList.ToArray());
      _goldMeshCombiner.mesh = goldMesh;
   }

   public static Vector2Int WorldToCell(Vector3 position)
   {
      MapUtil mapUtil = Instance;
      Vector2 newPos = new Vector2(position.x, position.z) + mapUtil.center;
      Vector2Int cellPos =  Vector2Int.FloorToInt(newPos);
      return cellPos;
   }


   public static MapBlockType WorldToCellType(Vector3 position)
   {
      Vector2Int cellPos = WorldToCell(position);
      return Instance.mapInfo[cellPos.x, cellPos.y];
   }

   


}
