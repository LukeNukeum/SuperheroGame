using UnityEngine;

namespace LupiLab.Character
{
    public class WalkingMovement : Movement
    {
        private static float _minimumInputSqrMagnitudeThreshold = 0.001f;
        private WalkingMovementInput _input;
        private Animator _animator;

        public WalkingMovement(Transform transform, WalkingMovementInput input, Animator animator)
        {
            _transform = transform;
            _input = input;
            _animator = animator;
        }

        public override void OnEnter()
        {
            
        }

        public override void Tick()
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
            {
                return;
            }

            Vector3 movementVector = _input.WorldMoveDirection;

            if (movementVector.sqrMagnitude > _minimumInputSqrMagnitudeThreshold)
            {
                _transform.forward = movementVector;
            }

            float walkSpeed = movementVector.magnitude;

            if (_input.IsSprinting)
            {
                walkSpeed = walkSpeed * 2;
            }

            _animator.SetFloat("WalkForwardSpeed", walkSpeed, 0.1f, Time.fixedDeltaTime);
            _animator.SetFloat("StrafeSidewaysSpeed", 0, 0.1f, Time.fixedDeltaTime);
        }

        public override void OnExit()
        {

        }
    }
}