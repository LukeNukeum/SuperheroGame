using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public abstract class Damageable : MonoBehaviour
    {
        

        protected Health _healthStat;

        public void SetHealthStatTarget(Health health)
        {
            _healthStat = health;
        }

        public virtual void DoDamage(float damageValue)
        {
            DoDamage(new Damage(damageValue));
        }

        public virtual void DoDamage(Damage damage)
        {
            _healthStat?.DoDamage(damage);
        }




        
    }
}