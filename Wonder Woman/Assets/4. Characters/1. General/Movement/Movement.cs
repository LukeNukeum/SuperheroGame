using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.StateMachines;

namespace LupiLab.Character
{
    public abstract class Movement: IState
    {

        protected Transform _transform;

        public abstract void OnEnter();
        public abstract void Tick();
        public abstract void OnExit();
    }
}