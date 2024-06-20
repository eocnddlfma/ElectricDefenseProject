using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoSingleton<WaveManager>
{
    public UIUtil uiManager;
    public float CurrentWaveTime;
    public float MaxWaveTime;
    public int _wave;
    float timeScale = 1;

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
        timeScale = PlayerPrefs.GetFloat("Difficulty", 1);
        _wave = 0;
        uiManager.viewCanvas.WaveGaugePercent = 0;

        StartCoroutine(UpdateWave());
    }

    public void GameEnd()
    {
        Time.timeScale = 0;
        if (PlayerPrefs.GetFloat("MaxWave", 1) < WaveManager.Instance._wave)
        {
            PlayerPrefs.SetFloat("MaxWave", WaveManager.Instance._wave);
        }
        
    }

    public IEnumerator UpdateWave()
    {
        while (_wave < 31)
        {
            while (CurrentWaveTime < MaxWaveTime)
            {
                CurrentWaveTime += Time.deltaTime *timeScale;
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

    public void SceneRestart()
    {
        SceneManager.LoadScene("Kbh");
    }
    public void SceneMainStart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
