using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class BlockingCharacterDamageable : CharacterDamageable
    {

        //[System.NonSerialized]
        public bool IsBlocking = false;

        [SerializeField] protected float _maximumBlockAngle = 45f;
        [SerializeField] DamageType[] _unblockableDamageTypes;

        protected CharacterStat _staminaStat;
        protected Transform _transform;

        public void SetStaminaStatTarget(CharacterStat staminaStat)
        {
            _staminaStat = staminaStat;
        }

        public void SetTransformTarget(Transform transform)
        {
            _transform = transform;
        }

        public override void DoDamage(Damage damage)
        {
            if (CanBlockDamage(damage))
            {
                _staminaStat.SetValue(_staminaStat.Value - damage.Value);
            }
            else
            {
                _healthStat?.DoDamage(damage);
            }
        }


        protected virtual bool CanBlockDamage(Damage damage)
        {
            if (!IsBlocking) return false;
            if (!transform) return false;
            if (_unblockableDamageTypes.Contains(damage.DamageType)) return false;
            if (damage.Direction == Vector3.zero) return false;
            if (_staminaStat.Value < damage.Value * 0.5f) return false;

            float localAttackAngle = Vector3.Angle(-damage.Direction, _transform.forward);
            if(localAttackAngle <= _maximumBlockAngle)
            {
                return true;
            }

            return false;
        }

    }
}