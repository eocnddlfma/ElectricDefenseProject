using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGeneratorManager : MonoSingleton<EnemyGeneratorManager>
{
    public List<Transform> GeneratePos;
    public List<Transform> AbleGeneratePos;
    [SerializeField] private float buildingCheckDistance;
    [SerializeField] private EnemyTableSO _enemyTableSO;

    private Dictionary<EnemyType, GameObject> _dictionary;
    
    void Awake()
    {
        AbleGeneratePos = new List<Transform>();
        
        _dictionary = new Dictionary<EnemyType, GameObject>();
        _enemyTableSO.list.ForEach((a) => _dictionary.Add(a.type, a.prefab));
        GeneratePos = transform.Find("EnemyGenerator").GetComponentsInChildren<Transform>().ToList();
        GeneratePos.RemoveAt(0);
    }

    private RaycastHit[] _raycastHits;
    public void CheckAblePos()
    {
        print("asdf");
        AbleGeneratePos.Clear();
        print("asdfds");
        foreach (var a in GeneratePos)
        {
            var hits = Physics.SphereCastNonAlloc(
                transform.position, buildingCheckDistance,//보이는 곳에 자원 빌딩이 있는가?
                Vector3.up, _raycastHits, 0f, 7);//layer7 == resourcebuilding
            print(hits);
            if (hits == 0)
            {
                AbleGeneratePos.Add(a);
            }
        }
    }

    public Vector3 GetRandomGeneratePos()
    {
        int a = Random.Range(0, AbleGeneratePos.Count-1);
        Vector3 pos = AbleGeneratePos[a].position;
        pos += new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        Debug.Log("randomPos :" + pos);
        pos = Vector3.Lerp(Vector3.zero, pos, WaveManager.Instance._wave / 14f);
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
