using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Core
{
    public class BasicSpawner : Spawner
    {
        [SerializeField] protected GameObject Prefab;

        public void SimpleSpawn()
        {
            Spawn();
        }

        public override bool Spawn()
        {
            if (!Prefab) return false;
            Instantiate(Prefab, transform.position, transform.rotation);
            return true;
        }

        public override bool Spawn(out GameObject[] objects)
        {
            if (!Prefab)
            {
                objects = new GameObject[0];
                return false;
            }
            objects = new GameObject[1];
            objects[0] = Instantiate(Prefab, transform.position, transform.rotation);
            return true;
        }
    }
}