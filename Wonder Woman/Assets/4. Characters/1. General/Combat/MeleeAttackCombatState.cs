using UnityEngine;

namespace LupiLab.Character
{
    public class MeleeAttackCombatState : CombatState
    {

        protected Animator _animator;

        public bool HasFinishedAttacking { get; protected set; } = false;
        public bool HasStartedAttacking { get; protected set; } = false;


        public MeleeAttackCombatState(Animator animator)
        {
            _animator = animator;
        }


        public override void OnEnter()
        {
            HasFinishedAttacking = false;
            HasStartedAttacking = false;
            _animator.Play("Attack1", 0, 0.1f);

        }

        public override void OnExit()
        {
        }

        public override void Tick()
        {
            if (!HasStartedAttacking && _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                HasStartedAttacking = true;
            }
            if(HasStartedAttacking && !_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                HasFinishedAttacking = true;
            }
        }
    }
}