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
    private float _stunTime;
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
        Debug.Log(_agent.status.name);
        maxHp = agent.status.maxHp;
        Hp = maxHp;
    }

    /** 즉시 데미지 */
    public void DoDamage(int damage)
    {
        Hp = hp - damage;
        if(hp<0){}
        //_agent.Die();
            
    }
    /** 딜레이 데미지 */
    public void DoDamage(float waitSecond ,int damage)
    {
        StartCoroutine(DamageTimer(waitSecond, damage));
        if(hp<0){}
        //_agent.Die();
            
    }
    /** 스턴 데미지 */
    public void DoDamage(int damage, float stunTime)
    {
        Hp = hp - damage;
        if(hp<0){}
            //_agent.Die();
        _stunTime = stunTime;
    }

    public void GetHeal(int heal)
    {
        Hp = hp + heal;
    }

    IEnumerator DamageTimer(float timer, int damage)
    {
        yield return new WaitForSeconds(timer);
        Hp = hp - damage;
    }

}
