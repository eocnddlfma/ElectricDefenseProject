using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGeneratorManager : MonoSingleton<EnemyGeneratorManager>
{
    private List<Transform> GeneratePos;
    private List<Transform> AbleGeneratePos;
    [SerializeField] private float buildingCheckDistance;
    [SerializeField] private EnemyTableSO _enemyTableSO;

    private Dictionary<EnemyType, GameObject> _dictionary;
    
    void Start()
    {
        _dictionary = new Dictionary<EnemyType, GameObject>();
        _enemyTableSO.list.ForEach((a) =>
        {
            _dictionary.Add(a.type, a.prefab);
        });
        GeneratePos = transform.GetComponentsInChildren<Transform>().ToList();
    }

    private RaycastHit[] _raycastHits;
    public void CheckAblePos()
    {
        AbleGeneratePos.Clear();
        foreach (var a in GeneratePos)
        {
            var hits = Physics.SphereCastNonAlloc(
                transform.position, buildingCheckDistance,//보이는 곳에 자원 빌딩이 있는가?
                Vector3.up, _raycastHits, 0f, 7);//layer7 == resourcebuilding

            if (hits == 0)
            {
                AbleGeneratePos.Add(a);
            }
        }
    }

    public Vector3 GetRandomGeneratePos()
    {
        int a = Random.Range(0, AbleGeneratePos.Count);
        Vector3 pos = AbleGeneratePos[a].position;
        pos += new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        Debug.Log("randomPos :" + pos);
        return pos;
    }

    public void GenerateEnemy(int num, EnemyType enemyType)
    {
        CheckAblePos();
        StartCoroutine(makeEnemy(num, enemyType));
    }

    private IEnumerator makeEnemy(int num, EnemyType enemyType)
    {
        GameObject g = _dictionary[enemyType];
        for (int i = 0; i < num; i++)
        {
            Instantiate(g, GetRandomGeneratePos(), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0,0.1f));
        }
        
    }
}
