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
    [SerializeField] private HealthBar _healthBar;

    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (value > maxHp)
                hp = maxHp;
            Debug.Log($"current hp {hp}");
            _healthBar.UpdateHealthbar(hp/(float)maxHp);
        } 
    }
    
    public void Initialize(Agent agent)
    {
        _agent = agent;
        maxHp = agent.agentStatus.maxHp;
        Hp = maxHp;
    }

    public void DoDamage(int damage)
    {
        Hp = hp - damage;
        if(hp<0)
            _agent.Die();
            
    }

    public void GetHeal(int heal)
    {
        Hp = hp + heal;
    }

}
