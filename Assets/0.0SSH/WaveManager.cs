using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public UIManager uiManager;
    public float CurrentWaveTime;
    public float MaxWaveTime;
    public int _wave;
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
            _wave++;
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
