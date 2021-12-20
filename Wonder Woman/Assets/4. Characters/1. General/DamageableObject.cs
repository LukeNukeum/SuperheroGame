using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class DamageableObject : Damageable
    {
        [SerializeField] protected float _maxHealth = 10;

        protected virtual void Awake()
        {
            _healthStat = new Health(_maxHealth, _maxHealth);
            _healthStat.DeathEvent += OnDeath;
        }

        protected void OnDeath(Health health)
        {
            Destroy(gameObject);
        }
    }
}