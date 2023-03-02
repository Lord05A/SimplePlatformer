using System;
using UnityEngine;

public class Health : MonoBehaviour
{   
    public int Value { get; private set; }
    public int MaxValue { get; } = 3;

    public event Action Dead;
    public event Action ValueChanged;

    public void Init()
    {
        Value = MaxValue;
        Dead = null; 
        ValueChanged = null;
    }
    
    public void ApplyDamage(int damage)
    {
        if (damage < 0)
        {
            damage = 0;
        }

        Value -= damage;

        if (Value < 0)
        {
            Value = 0;
            Dead?.Invoke();
        }

        ValueChanged?.Invoke();
    }

    public void HealUp(int heal)
    {
        if (heal < 0)
        {
            heal = 0;
        }

        Value += heal;

        if (Value >= MaxValue)
        {
            Value = MaxValue;
        }

        ValueChanged?.Invoke();
    }    
}
