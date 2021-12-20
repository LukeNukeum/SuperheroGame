using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character.Motion
{
    public abstract class CharacterWalkMotionHandler
    {

        public abstract bool IsGrounded { get; }

        public abstract void FixedMove(Vector3 velocity);

        [System.Serializable]
        public class GroundScanSettings
        {
            public LayerMask WalkableLayerMask;
            public float GroundScanRadius = 0.5f;
            public float MaxGroundScanDistance = 0.2f;
            public float MaxStepHeight = 0.3f;
            public float GroundScanVerticalOffset = 0.1f;
        }
    }
}