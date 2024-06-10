using UnityEngine;

public interface IBuildingAgent
{
   Vector2Int cellPosition { get; set; }
   void Die();
   void Upgrade();
   void ShowDebug();
   void HideDebug();
}