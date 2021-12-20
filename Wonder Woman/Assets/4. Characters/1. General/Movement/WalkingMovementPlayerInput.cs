using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Camera;

namespace LupiLab.Character
{
    [System.Serializable]
    public class WalkingMovementPlayerInput : WalkingMovementInput
    {

        public override bool DoJump => _doJump;
        public override bool IsSprinting => !IsStrafing && Input.GetButton("Sprint");
        public override bool IsStrafing => Input.GetButton("Fire2");
        public override Vector3 LocalMoveDirection => _localMoveDirection;
        public override Vector3 WorldMoveDirection
        {
            get
            {
                if (CameraRig)
                {
                    return CameraRig.ForwardFlatAngle * _localMoveDirection;
                }
                else
                {
                    return _localMoveDirection;
                }
            }
        }

        [SerializeField] protected SOFloat _inputDoublePressTime;
        public CameraRig CameraRig { get; protected set; }

        protected bool _doJump = false;
        protected Vector3 _localMoveDirection = Vector3.zero;

        protected bool _hasFirstFlyingTogglePressDone = false;
        protected float _latestTimeOfSecondFlyingTogglePress = -1000;


        //debug
        int _timesJumped = 0;
        int _timesToggledFly = 0;



        public WalkingMovementPlayerInput(CameraRig cameraRig, SOFloat inputDoublePressTime)
        {
            CameraRig = cameraRig;
            _inputDoublePressTime = inputDoublePressTime;
        }

        

        public override void UpdateTick()
        {

            if (_hasFirstFlyingTogglePressDone && _latestTimeOfSecondFlyingTogglePress < Time.unscaledTime)
                _hasFirstFlyingTogglePressDone = false;

            if (Input.GetButtonDown("Jump"))
            {
                if (!_hasFirstFlyingTogglePressDone)
                {
                    _doJump = true;
                    _hasFirstFlyingTogglePressDone = true;
                    _latestTimeOfSecondFlyingTogglePress = Time.unscaledTime + _inputDoublePressTime.Value;
                }
                else
                {
                    _hasFirstFlyingTogglePressDone = false;

                    InvokeToggleFlyingInputEvent();
                }
            }
        }

        public override void FixedUpdateTick()
        {
            _localMoveDirection = GetLocalMoveDirection();

            if (DoJump) _timesJumped++;

        }

        public override void ResetFlags()
        {
            _doJump = false;
        }

        public void SetCameraRig(CameraRig cameraRig)
        {
            CameraRig = cameraRig;
        }

        public Vector3 GetLocalMoveDirection()
        {
            Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            return Vector3.ClampMagnitude(inputVector, 1);
        }

    }
}