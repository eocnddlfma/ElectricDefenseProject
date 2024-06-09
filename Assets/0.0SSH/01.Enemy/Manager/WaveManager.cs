using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
    public UIManager uiManager;
    public float CurrentWaveTime;
    public float MaxWaveTime;
    public int _wave;

    public List< List< Tuple<int, EnemyType>>> EnemyPerWave;//이거 데이터 어떻게 넣을건지 상의
    private void Start()
    {
        _wave = 0;
        //uiManager.viewCanvas.WaveGaugePercent = 0;

        StartCoroutine(UpdateWave());
    }

    public IEnumerator UpdateWave()
    {
        while (_wave < 31)
        {
            _wave++;
            EnemyPerWave[_wave].ForEach( a=> EnemyGeneratorManager.Instance.GenerateEnemy(a.Item1, a.Item2));
            uiManager.viewCanvas.WaveText = _wave.ToString();
            while (CurrentWaveTime < MaxWaveTime)
            {
                CurrentWaveTime += Time.deltaTime;
                uiManager.viewCanvas.WaveGaugePercent = CurrentWaveTime / MaxWaveTime;
                yield return null;
            }

            CurrentWaveTime = 0;
        }
    }
}
