using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 골드를 관리하는 스크립트
public class GoldManager : MonoSingleton<GoldManager>
{
    [Header("골드 보유량을 표기할 텍스트 컴포넌트")]
    public TextMeshProUGUI GoldText;
    [SerializeField] private float goldAmount;
    
    
    void Update()
    {
        GoldText.text = "Gold: "+goldAmount.ToString();
    }

    public void AddGold(int amount)
    {
        goldAmount += amount;
    }
    public void SubtactGold(int amount)
    {
        goldAmount -= amount;
    }
    
    
}
