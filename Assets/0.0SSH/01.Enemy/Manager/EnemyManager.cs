using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField]public List<Enemy> enemyList;

    private List<Transform> GeneratePos;
    
    
    
    public void GenerateEnemy(int wave)
    {
        int pos = GeneratePos.Count;
        for (int i = 0; i < wave; i++)
        {
            int rnum = Random.Range(0, pos);

            
        }
    }
    
}
