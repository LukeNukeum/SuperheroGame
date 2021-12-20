using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class WalkingMovementAIInput : WalkingMovementInput
    {


        public override bool DoJump => false;

        public override bool IsSprinting => false;

        public override bool IsStrafing => false;

        public override Vector3 LocalMoveDirection => throw new System.NotImplementedException();

        public override Vector3 WorldMoveDirection => throw new System.NotImplementedException();




        public override void FixedUpdateTick()
        {
            throw new System.NotImplementedException();
        }

        public override void ResetFlags()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateTick()
        {
            throw new System.NotImplementedException();
        }
    }
}