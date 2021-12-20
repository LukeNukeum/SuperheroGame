using UnityEngine;
using LupiLab.Camera;

namespace LupiLab.Character
{
    public class WalkingStrafeMovement : Movement
    {
        private WalkingMovementInput _input;
        private Animator _animator;
        private CameraRig _cameraRig; //Todo - rely on input script to determine forward direction, don't directly use the camera because AI agents may use strafing movement

        public WalkingStrafeMovement(Transform transform, WalkingMovementInput input, Animator animator, CameraRig cameraRig)
        {
            _transform = transform;
            _input = input;
            _animator = animator;
            _cameraRig = cameraRig;
        }

        public override void OnEnter()
        {
            
        }

        public override void Tick()
        {

            Vector3 movementVector = _input.LocalMoveDirection;

            if (movementVector != Vector3.zero)
            {
                _transform.forward = _cameraRig.ForwardFlatAngle * Vector3.forward;
            }
            _animator.SetFloat("WalkForwardSpeed", movementVector.z, 0.1f, Time.fixedDeltaTime);
            _animator.SetFloat("StrafeSidewaysSpeed", movementVector.x, 0.1f, Time.fixedDeltaTime);

        }

        public override void OnExit()
        {
            
        }

    }
}