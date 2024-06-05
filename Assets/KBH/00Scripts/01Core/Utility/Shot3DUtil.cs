using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot3DUtil : MonoSingleton<Shot3DUtil>
{
   [System.Serializable]
   struct CursorVisualShotInfo
   {
      public ToolBarEnum cursorType;
      public CursorShotVisual cursorVisual;
   }


   [System.Serializable]
   struct MeshAboutAgentType
   {
      public AgentType agentType;
      public Mesh mesh;
   }

   private Camera main;
   public static bool isActive = false;
   [SerializeField] private LayerMask _whatIsCursorTarget;
   [SerializeField] private LayerMask _whatIsIgnoreTarget;
   
   [SerializeField] private List<CursorVisualShotInfo> _cursorShotVisualList;
   private Dictionary<ToolBarEnum, CursorShotVisual> _cursorShotVisualDictionary;

   [SerializeField] private List<MeshAboutAgentType> _meshAboutAgentList;
   private Dictionary<AgentType, Mesh> _meshAboutAgentDictionary;
   [SerializeField] private MeshFilter _meshDrawTarget;

   public static Vector2Int cursorCellPosition;
   public static AgentType cursorCellType;
   public static Vector3 DrawingMeshPosition
      => Instance._meshDrawTarget.transform.position;
   public static CursorShotVisual currentCursorShotVisual
      => Instance._cursorShotVisualDictionary[Instance.currentCursorShotType];

   public ToolBarEnum currentCursorShotType;
   public AgentType currentDrawingMeshType;

   private void Awake()
   {
      Initialize();
   }


   private void Initialize()
   {
      main = Camera.main;
      _cursorShotVisualDictionary = new Dictionary<ToolBarEnum, CursorShotVisual>();
      for(int i = 0; i<_cursorShotVisualList.Count; ++i)
      {
         _cursorShotVisualDictionary[_cursorShotVisualList[i].cursorType]
            = _cursorShotVisualList[i].cursorVisual;
      }

      _meshAboutAgentDictionary = new Dictionary<AgentType, Mesh>();
      for (int i = 0; i < _meshAboutAgentList.Count; ++i)
      {
         _meshAboutAgentDictionary[_meshAboutAgentList[i].agentType]
            = _meshAboutAgentList[i].mesh;
      }
   }

   private void Update()
   {
      if (!isActive)
      {
        _meshDrawTarget.gameObject.SetActive(false);
      }
      else
      {
         _meshDrawTarget.gameObject.SetActive(true);
      }

      Vector3 samplePosition = CursorRayPosition();
      Vector2Int targetCellPosition = MapUtil.WorldToCell(samplePosition);
      Vector3 meshDrawTargetPosition = MapUtil.CellToWorld(targetCellPosition)
         + samplePosition.y * Vector3.up;


      _meshDrawTarget.transform.position =  meshDrawTargetPosition;
      _cursorShotVisualDictionary[currentCursorShotType].transform.position
         = meshDrawTargetPosition;
      cursorCellPosition = targetCellPosition;
      cursorCellType = MapUtil.Instance[cursorCellPosition.x, cursorCellPosition.y];
   }

   public static void SetCursorShotVisual(ToolBarEnum cursorVisualType)
   {
      if (cursorVisualType != Instance.currentCursorShotType)
      {
         CursorShotVisual targetShotVisual;

         if (Instance._cursorShotVisualDictionary
            .TryGetValue(Instance.currentCursorShotType, out targetShotVisual))
         {
            targetShotVisual.SetShow(false, 0.5f);
         }

         if (Instance._cursorShotVisualDictionary
            .TryGetValue(cursorVisualType, out targetShotVisual))
         {
            targetShotVisual.SetShow(true, 0.5f);
         }

         Instance.currentCursorShotType = cursorVisualType;
      }
   }

   public static void SetDrawingMesh(AgentType meshType)
   {
      if(meshType != Instance.currentDrawingMeshType)
         Instance._meshDrawTarget.mesh = Instance._meshAboutAgentDictionary[meshType];
   }


   public static Vector3 CursorRayPosition()
   {
      Ray ray = Instance.main.ScreenPointToRay(InputUtil.MousePosition);
      if(Physics.Raycast(ray, out RaycastHit hitInfo, 
         Instance.main.farClipPlane, Instance._whatIsCursorTarget & ~Instance._whatIsIgnoreTarget))
      {
         return hitInfo.point;
      }
      return Vector3.zero;
   }

   public static Agent GetAgentOnCurrentCursor()
   {
      return MapUtil.Instance[cursorCellPosition];
   }

}
