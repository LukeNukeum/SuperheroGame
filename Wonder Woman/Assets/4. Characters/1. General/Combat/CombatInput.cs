using System;
using UnityEngine;

namespace LupiLab.Character
{
    public class CombatInput: IInput
    {
        public bool HasBlockInput { get; private set; }

        public event Action AttackInputEvent;     //Todo: consider adding a parameter to determine if player should do a heavy attack
        public event Action HeldItemToggleInputEvent;
        public event Action<int> SwitchToHeldItemInputEvent;

        public void UpdateTick()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //HasAttackInput = true;
                AttackInputEvent?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //HasSwitchWeaponInput = true;
                HeldItemToggleInputEvent?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchToHeldItemInputEvent?.Invoke(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchToHeldItemInputEvent?.Invoke(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchToHeldItemInputEvent?.Invoke(2);
        }

        public void FixedUpdateTick()
        {
            HasBlockInput = Input.GetButton("Fire2");

        }

        public void ResetFlags()
        {
        }
    }
}