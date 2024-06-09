using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemySO/EnemyType")]
public class EnemyTypeSO : ScriptableObject
{
    public GameObject prefab;
    public EnemyType type;
}
