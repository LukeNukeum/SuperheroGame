using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class WeaponSwitchingState : CombatState
    {

        // Currently this script handles:
        // Combat state
        // Tracking current weapon
        // Receiving the switch event



        

        public bool HasFinishedSwitching { get; protected set; } = false;
        public bool HasStartedSwitching { get; protected set; } = false;

        public bool IsHoldingSword { get; private set; } = false;

        private string _switchAnimationName = "Sheath Sword";
        private Animator _animator;

        public WeaponSwitchingState(Animator animator)
        {
            _animator = animator;
        }

        public override void OnEnter()
        {
            HasFinishedSwitching = false;
            HasStartedSwitching = false;
            _animator.Play(_switchAnimationName);
        }

        public override void OnExit()
        {
            //throw new System.NotImplementedException();
        }

        public override void Tick()
        {
            //throw new System.NotImplementedException();
            if (!HasStartedSwitching && _animator.GetCurrentAnimatorStateInfo(1).IsTag("WeaponSwitching"))
            {
                HasStartedSwitching = true;
            }
            if (HasStartedSwitching && !_animator.GetCurrentAnimatorStateInfo(1).IsTag("WeaponSwitching"))
            {
                HasFinishedSwitching = true;
            }
        }

        public void SetSwitchAnimationName(string newName)
        {
            _switchAnimationName = newName;
        }

        
    }
}