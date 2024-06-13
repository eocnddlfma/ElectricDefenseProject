using System;
using UnityEngine;

public delegate Transform UpgradeEventDelegate(Vector2Int position);

[System.Serializable]
public class PlayerUpgradeInfo
{
   public UpgradeEventDelegate UpgradeEvent;
   public Action EndUpgradeEvent;
}
