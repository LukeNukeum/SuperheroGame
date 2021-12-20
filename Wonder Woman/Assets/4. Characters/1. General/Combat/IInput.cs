using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public interface IInput
    {
        void UpdateTick();
        void FixedUpdateTick();
        void ResetFlags();
    }
}