using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class CharacterStat
    {
        [SerializeField] protected float _value = 100;
        [SerializeField] protected float _maxValue = 100;

        public float Value { get { return _value; } }
        public float MaxValue { get { return _maxValue; } }

        public event Action<CharacterStat, float, float> ValueChangedEvent; // Parameters typically filled with this, old value, new value.

        public CharacterStat(float maxValue, float value)
        {
            _maxValue = maxValue;
            _value= Mathf.Clamp(value, 0, maxValue);
        }

        public virtual void SetValue(float newValue)
        {
            float oldValue = _value;
            _value = Mathf.Clamp(newValue, 0, _maxValue);
            ValueChangedEvent?.Invoke(this, oldValue, newValue);
        }


    }
}