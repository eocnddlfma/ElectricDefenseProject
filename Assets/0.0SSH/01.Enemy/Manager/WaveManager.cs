using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    public PlayerUI uiManager;
    public float CurrentWaveTime;
    public float MaxWaveTime;
    public int _wave;

    [SerializeField] private List<EnemyWaveInfoList> EnemyPerWave;//이거 데이터 어떻게 넣을건지 상의

    [System.Serializable]
    struct EnemyWaveInfo
    {
        public EnemyType enemy;
        public int num;
    }

    [System.Serializable]
    struct EnemyWaveInfoList
    {
        public List<EnemyWaveInfo> _enemyWaveInfos;
    }
    private void Start()
    {
        _wave = 0;
        uiManager.viewCanvas.WaveGaugePercent = 0;

        StartCoroutine(UpdateWave());
    }

    public IEnumerator UpdateWave()
    {
        while (_wave < 31)
        {
            while (CurrentWaveTime < MaxWaveTime)
            {
                CurrentWaveTime += Time.deltaTime;
                uiManager.viewCanvas.WaveGaugePercent = CurrentWaveTime / MaxWaveTime;
                yield return null;
            }
            _wave++;
            EnemyPerWave[_wave]._enemyWaveInfos.ForEach( a=> EnemyGeneratorManager.Instance.GenerateEnemy(a.num, a.enemy));
            uiManager.viewCanvas.WaveText = _wave.ToString();
            
            CurrentWaveTime = 0;
            MaxWaveTime = _wave + 10;
        }
    }
}
