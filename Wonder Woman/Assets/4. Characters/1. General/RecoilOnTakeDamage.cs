using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class RecoilOnTakeDamage
    {
        Health _health;
        Animator _animator;
        MovementManager _movementManager;
        RecoilMode[] _recoilModes;


        public RecoilOnTakeDamage(Health health, Animator animator, MovementManager movementManager, RecoilMode[] recoilModes)
        {
            _health = health;
            _health.TakeDamageEvent += OnTakeDamage;
            _animator = animator;
            _movementManager = movementManager;
            _recoilModes = recoilModes;
        }


        void OnTakeDamage(Health health, Damage damage)
        {
            if (health.IsDead) return;
            if (!_movementManager.IsGrounded) return;
            foreach(RecoilMode recoilMode in _recoilModes)
            {
                if (damage.Value >= recoilMode.DamageThreshold)
                {
                    _animator.Play(recoilMode.AnimationStateName, recoilMode.Layer, 0.1f);
                    break;
                }
            }
        }
    }

    [System.Serializable]
    public class RecoilMode
    {
        public string AnimationStateName = "Get Hit";
        public int Layer = 0;
        public float DamageThreshold = 20f;
    }
}