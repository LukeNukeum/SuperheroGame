using LupiLab.Character.Motion;
using LupiLab.Combat;
using UnityEngine;

namespace LupiLab
{
    public class AnimatorAssistant : MonoBehaviour
    {
        public bool ApplyRootMotion = true;

        public CharacterWalkMotionHandler MotionHandler;

        
        private MeleeAttack _meleeAttack;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _meleeAttack = GetComponentInParent<MeleeAttack>();
        }

        private void OnAnimatorMove()
        {
            if (!ApplyRootMotion)
            {
                return;
            }

            MotionHandler.FixedMove(_animator.velocity);
        }



    }
}