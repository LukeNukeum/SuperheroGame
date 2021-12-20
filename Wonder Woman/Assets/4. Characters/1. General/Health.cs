using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LupiLab.Character
{
    public class Health : CharacterStat
    {
        public Health(float maxValue, float value) : base(maxValue, value)
        {
            _maxValue = maxValue;
            _value = Mathf.Clamp(value, 0, maxValue);
        }

        public bool IsDead { get; private set; }

        public event Action<Health, Damage> TakeDamageEvent;
        public event Action<Health> DeathEvent;

        public virtual void DoDamage(Damage damage)
        {
            if (IsDead) return;
            SetValue(_value - damage.Value);

            if (_value == 0)
            {
                Kill();
            }
            TakeDamageEvent?.Invoke(this, damage);
        }


        public virtual void Kill()
        {
            if (IsDead) return;
            SetValue(0);
            IsDead = true;
            DeathEvent?.Invoke(this);
        }
    }
}