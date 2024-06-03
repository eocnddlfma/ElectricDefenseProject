using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;

    public float volume;
    void Start()
    {
        _volumeSlider.onValueChanged.AddListener(SetValue);
    }

    public void SetValue(float value)
    {
        volume = value;
    }

    private void OnDestroy()
    {
        _volumeSlider.onValueChanged.RemoveListener(SetValue);
    }
}
