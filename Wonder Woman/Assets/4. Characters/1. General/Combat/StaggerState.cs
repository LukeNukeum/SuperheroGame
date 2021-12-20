using System;
using UnityEngine;

namespace LupiLab.Character
{
    public class StaggerState : CombatState
    {
        protected bool _hasStartedStagger;
        protected Animator _animator;


        public event Action StaggerFinishedEvent;
            

        public StaggerState(Animator animator)
        {
            _animator = animator;
        }



        public override void OnEnter()
        {
            _hasStartedStagger = false;
            _animator.Play("Stagger", 0, 0.1f);
        }

        public override void OnExit()
        {

        }

        public override void Tick()
        {
            if (!_hasStartedStagger)
            {
                _hasStartedStagger = _animator.GetCurrentAnimatorStateInfo(0).IsTag("STAGGERED");
                return;
            }
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("STAGGERED"))
                StaggerFinishedEvent?.Invoke();
        }
    }
}