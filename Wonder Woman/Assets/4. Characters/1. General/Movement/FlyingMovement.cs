using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character.Motion
{
    public class FlyingMovement : Movement
    {
        private Animator _animator;
        private FlyingMovementInput _input;
        private FlySettings _settings;
        private AnimationMotionReceiver _animationMotionReceiver;
        private CharacterController _characterController;
        private CharacterControllerPhysics _physics;

        public FlyingMovement(Transform transform, Rigidbody rigidbody,FlyingMovementInput input, Animator animator, FlySettings settings, AnimationMotionReceiver animationMotionReceiver, CharacterController characterController, CharacterControllerPhysics physics)
        {
            _transform = transform;
            _input = input;
            _animator = animator;
            _settings = settings;
            _animationMotionReceiver = animationMotionReceiver;
            _characterController = characterController;
            _physics = physics;
        }

        public override void OnEnter()
        {
            //_rigidbody.useGravity = false;
            _animator.SetBool("IsFlying", true);
            _animationMotionReceiver.ApplyRootMotion = false;
            _physics.Velocity += Vector3.up * _settings.TakeOffVelocityIncrease;
            //_rigidbody.AddForce(Vector3.up * _settings.TakeOffVelocityIncrease, ForceMode.VelocityChange);
        }

        public override void Tick()
        {
            

            Vector3 targetVelocity = _input.GetWorldMoveDirection() * _settings.FlySpeed;
            Vector3 velocity = Vector3.MoveTowards(_physics.Velocity, targetVelocity, _settings.AirAcceleration*Time.fixedDeltaTime);

             

            Vector3 oldPosition = _transform.position;
            _characterController.Move(velocity * Time.fixedDeltaTime);
            _physics.Velocity = (_transform.position - oldPosition) / Time.fixedDeltaTime;

            //_rigidbody.AddForce(GetAirAcceleration(targetVelocity, _rigidbody.velocity), ForceMode.Acceleration);

            //Vector3 forwardVector = _rigidbody.velocity;
            //forwardVector.y = 0;
            //if (forwardVector.sqrMagnitude > 0.1f)
            //{
            //    _transform.forward = forwardVector;
            //}
        }

        public override void OnExit()
        {
            //_rigidbody.useGravity = true;
            _animator.SetBool("IsFlying", false);
            _animationMotionReceiver.ApplyRootMotion = true;
        }

        Vector3 GetAirAcceleration(Vector3 targetVelocity, Vector3 currentVelocity)
        {
            Vector3 _velocityChange = targetVelocity - currentVelocity;
            _velocityChange /= Time.fixedDeltaTime;
            return Vector3.ClampMagnitude(_velocityChange, _settings.AirAcceleration);

        }






        [System.Serializable]
        public class FlySettings
        {
            public float FlySpeed = 5;
            public float AirAcceleration = 5;
            public float TakeOffVelocityIncrease = 3f;
            public FlySettings(float flySpeed)
            {
                FlySpeed = flySpeed;
            }
        }
    }
}