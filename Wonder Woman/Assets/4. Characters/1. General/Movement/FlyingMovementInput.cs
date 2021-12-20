using System;
using UnityEngine;

namespace LupiLab.Character
{
    public abstract class FlyingMovementInput : IInput
    {
        public abstract Vector3 GetWorldMoveDirection();
        public abstract Vector3 GetLocalMoveDirection();
        public abstract bool IsStrafing { get; }

        public abstract void UpdateTick();
        public abstract void FixedUpdateTick();
        public abstract void ResetFlags();
    }
}