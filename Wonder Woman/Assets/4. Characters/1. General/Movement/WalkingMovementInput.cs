using System;
using UnityEngine;

namespace LupiLab.Character
{
    [System.Serializable]
    public abstract class WalkingMovementInput: IInput
    {
        public event Action ToggleFlyingInputEvent;

        public abstract bool DoJump { get; }
        public abstract bool IsSprinting { get; }
        public abstract bool IsStrafing { get; }
        public abstract Vector3 LocalMoveDirection { get; }
        public abstract Vector3 WorldMoveDirection { get; }

        public abstract void UpdateTick();
        public abstract void FixedUpdateTick();
        public abstract void ResetFlags();

        protected void InvokeToggleFlyingInputEvent()
        {
            ToggleFlyingInputEvent?.Invoke();
        }
        #region Debug
        public virtual void OnGUI()
        {
            GUILayout.Label($"Do jump: {DoJump}");
            GUILayout.Label($"Is sprinting: {IsSprinting}");
            GUILayout.Label($"Is strafing: {IsStrafing}");
            //GUILayout.Label($"Do toggle flying: {DoToggleFlying}");
        }
        #endregion
    }
}