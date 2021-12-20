using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Character;

namespace LupiLab.Combat
{
    public class MeleeAttack : MonoBehaviour
    {


        [SerializeField] private Transform _attackOriginPoint;
        [SerializeField] private float _meleeRange = 2f;
        [SerializeField] LayerMask _rayscanMask;
        [SerializeField] private float _damageValue = 40f;
        [SerializeField] private DamageType _damageType;

        private Character.Character _character;

        public void Awake()
        {
            if (!_attackOriginPoint)
            {
                _attackOriginPoint = transform;
            }
            _character = GetComponent<Character.Character>();

            AnimationEventReceiver animationEventReceiver = GetComponentInChildren<AnimationEventReceiver>();
            if (animationEventReceiver)
            {
                animationEventReceiver.HitEvent += OnMeleeHit;
            }
        }

        public void StartAttack()
        {

        }

        public void OnMeleeHit()
        {


            RaycastHit hitInfo;

            if (Physics.Raycast(_attackOriginPoint.position, _attackOriginPoint.forward, out hitInfo, _meleeRange, _rayscanMask))
            {
                Debug.DrawRay(_attackOriginPoint.position, _attackOriginPoint.forward * _meleeRange, Color.red, 1f);
                Damageable targetDamageable = hitInfo.collider.GetComponent<Damageable>();
                if (targetDamageable != null)
                {
                    Damage damage = new Damage(_damageValue, _damageType, _character, collider: hitInfo.collider, direction: _attackOriginPoint.forward);
                    targetDamageable.DoDamage(damage);
                }
            }
            else
            {
                Debug.DrawRay(_attackOriginPoint.position, _attackOriginPoint.forward * _meleeRange, Color.grey, 1f);
            }


        }
    }
}