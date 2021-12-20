using System;
using UnityEngine;

namespace LupiLab.Character
{
    [RequireComponent(typeof(Animator))]
    public class AnimationEventReceiver : MonoBehaviour
    {
        
        public event Action FootLEvent;
        public event Action FootREvent;
        public event Action HitEvent;
        public event Action WeaponSwitchEvent;

        void FootL()
        {
            FootLEvent?.Invoke();
        }
        void FootR()
        {
            FootREvent?.Invoke();
        }
        void Hit()
        {
            HitEvent?.Invoke();
        }
        void WeaponSwitch()
        {
            WeaponSwitchEvent?.Invoke();
        }

    }
}