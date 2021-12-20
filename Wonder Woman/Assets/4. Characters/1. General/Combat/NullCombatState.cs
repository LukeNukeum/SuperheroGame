using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.StateMachines;

namespace LupiLab.Character
{
    public class NullCombatState : CombatState
    {

        public override void OnEnter()
        {
            //Debug.Log("Entering null combat state");
        }

        public override void Tick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}