using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


public class Health : MonoBehaviour
{
    [SerializeField] private int maxHp; 
    [SerializeField] private int hp;
    [SerializeField] private Agent _agent;

    public int Hp
    {
        get => hp;
        set => hp = value;
    }
    
    public void Initialize(Agent agent)
    {
        _agent = agent;
        maxHp = agent.agentStatus.maxHp;
        hp = maxHp;
    }
    private void Awake()
    {
        
    }

    public void GetDamage()
    {
        
    }

    public void GetHeal()
    {
        
    }

}
