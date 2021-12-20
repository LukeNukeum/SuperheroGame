using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class Damage
    {
        public float Value { get; protected set; }
        public DamageType DamageType { get; protected set; }
        public Character Attacker { get; protected set; }
        public Collider Collider { get; protected set; }
        public Vector3 Direction { get; protected set; }


        public Damage(float value)
        {
            Value = value;
        }

        public Damage(float value, DamageType damageType)
        {
            Value = value;
            DamageType = damageType;
        }

        public Damage(float value, Character attacker)
        {
            Value = value;
            Attacker = attacker;
        }

        public Damage(float value, DamageType damageType, Character attacker)
        {
            Value = value;
            DamageType = damageType;
            Attacker = attacker;
        }

        public Damage(float value = 0, DamageType damageType = null, Character attacker = null, Collider collider = null)
        {
            Value = value;
            DamageType = damageType;
            Attacker = attacker;
            Collider = collider;
        }

        public Damage(float value, DamageType damageType, Character attacker, Collider collider, Vector3 direction)
        {
            Value = value;
            DamageType = damageType;
            Attacker = attacker;
            Collider = collider;
            Direction = direction;
        }


        public override string ToString()
        {
            string str = Value.ToString();
            if (DamageType != null) str += $" {DamageType.name}";
            str += " damage";
            if (Attacker != null) str += $" from {Attacker.name}";
            return str;
        }

    }
}