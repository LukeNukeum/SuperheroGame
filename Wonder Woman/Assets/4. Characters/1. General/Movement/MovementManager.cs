using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public abstract class MovementManager : MonoBehaviour
    {


        public abstract bool IsGrounded { get; }

        public virtual void Initialize()
        {

        }

        public virtual void UpdateTick()
        {
            
        }

        public virtual void FixedUpdateTick()
        {

        }

        

    }
}