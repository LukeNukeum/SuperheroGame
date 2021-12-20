using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.StateMachines;

namespace LupiLab.Character
{
    public abstract class CombatState : IState
    {
        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Tick();

    }
}