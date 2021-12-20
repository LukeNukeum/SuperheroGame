using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class PlayAnimationOnDeath
    {
        Health _health;
        Animator _animator;

        public PlayAnimationOnDeath(Health health, Animator animator)
        {
            _health = health;
            _health.DeathEvent += OnDeath;
            _animator = animator;

        }

        void OnDeath(Health health)
        {

            _animator.Play("Die", 0, 0.1f);
        }
    }
}