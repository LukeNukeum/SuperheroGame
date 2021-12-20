using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LupiLab.Core
{
    public abstract class Spawner : MonoBehaviour
    {

        public abstract bool Spawn();

        public abstract bool Spawn(out GameObject[] objects);

    }
}