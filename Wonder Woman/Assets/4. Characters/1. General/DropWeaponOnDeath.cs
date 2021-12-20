using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public class DropWeaponOnDeath : MonoBehaviour
    {

        [SerializeField] private GameObject _weaponPrefab;

        [SerializeField] private float _maxThrownVelocity = 2f;

        private void Start()
        {
            Health health = GetComponentInParent<Character>().Health;
            health.DeathEvent += OnDeath;
        }

        void OnDeath(Health health)
        {
            if (isActiveAndEnabled)
            {
                GameObject spawnedObject = Instantiate(_weaponPrefab, transform.position, transform.rotation);
                Rigidbody rigidbody = spawnedObject.GetComponent<Rigidbody>();
                if(rigidbody!= null)
                {
                    rigidbody.velocity = Random.insideUnitSphere * _maxThrownVelocity;
                }
                gameObject.SetActive(false);
                enabled = false;
            }
        }


    }
}