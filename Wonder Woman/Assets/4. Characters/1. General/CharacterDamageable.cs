using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class CharacterDamageable : Damageable
    {

        public override void DoDamage(Damage damage)
        {
            base.DoDamage(damage);
            Debug.DrawLine(transform.position, transform.position + damage.Direction, Color.red, 2f);
        }
    }
}