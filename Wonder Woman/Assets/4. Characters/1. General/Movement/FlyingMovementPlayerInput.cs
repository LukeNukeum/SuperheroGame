using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Camera;

namespace LupiLab.Character
{
    public class FlyingMovementPlayerInput : FlyingMovementInput
    {
        public CameraRig CameraRig;

        public override bool IsStrafing => Input.GetButtonDown("Fire2");

        public FlyingMovementPlayerInput()
        {

        }

        public FlyingMovementPlayerInput(CameraRig cameraRig)
        {
            CameraRig = cameraRig;
        }

        public override void UpdateTick()
        {
            
        }

        public override void FixedUpdateTick()
        {
            
        }

        public override void ResetFlags()
        {
            
        }

        public override Vector3 GetWorldMoveDirection()
        {
            if (CameraRig != null)
            {
                return CameraRig.TrueForwardAngle * GetLocalMoveDirection();
            }

            return GetLocalMoveDirection();
        }

        public override Vector3 GetLocalMoveDirection()
        {
            Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump") - Input.GetAxis("Crouch"), Input.GetAxis("Vertical"));

            return Vector3.ClampMagnitude(inputVector, 1);
        }
    }
}