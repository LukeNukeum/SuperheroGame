using LupiLab.Character.Motion;
using UnityEngine;

namespace LupiLab.Character
{
    [RequireComponent(typeof(Animator))]
    public class AnimationMotionReceiver : MonoBehaviour
    {
        public bool ApplyRootMotion = true;

        public CharacterWalkMotionHandler MotionHandler;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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