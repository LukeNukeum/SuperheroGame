using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class BlockingCombatState : CombatState
    {

        protected Animator _animator;
        protected BlockingCharacterDamageable _blockingCharacterDamageable;

        public BlockingCombatState(Animator animator, BlockingCharacterDamageable blockingCharacterDamageable)
        {
            _animator = animator;
            _blockingCharacterDamageable = blockingCharacterDamageable;
            if (_blockingCharacterDamageable) _blockingCharacterDamageable.IsBlocking = false;
        }



        public override void OnEnter()
        {
            if (_animator) _animator.SetBool("IsBlocking",true);
            if (_blockingCharacterDamageable) _blockingCharacterDamageable.IsBlocking = true;
        }

        public override void OnExit()
        {
            if (_animator) _animator.SetBool("IsBlocking", false);
            if (_blockingCharacterDamageable) _blockingCharacterDamageable.IsBlocking = false;
        }

        public override void Tick()
        {
            // throw new System.NotImplementedException();
        }
    }
}