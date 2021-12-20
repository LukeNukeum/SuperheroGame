using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LupiLab.Character
{
    [RequireComponent(typeof(Health))]
    public class EvokeUnityEventOnDeath : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnDeathEvent;

        private void OnEnable()
        {
            Health health = GetComponent<Health>();
            if(health != null)
            {
                health.DeathEvent += OnDeath;
            }
        }

        private void OnDeath(Health health)
        {
            OnDeathEvent?.Invoke();
        }

        private void OnDisable()
        {
            Health health = GetComponent<Health>();
            if (health != null)
            {
                health.DeathEvent -= OnDeath;
            }
        }
    }
}