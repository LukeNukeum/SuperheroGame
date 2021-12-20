using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LupiLab.Core;

namespace LupiLab.Character
{
    public class PlayerCharacter : Character
    {
        protected Animator _animator;               // Can be on a child object or on the root object
        protected CombatManager _combatManager;
        //protected MovementManager _movementManager; // Behaviour should be attached to the root object if part of the character

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponentInChildren<Animator>();

            SetUpCombatManager();
            SetUpMovementManager();




            Health.DeathEvent += DisableMovementOnDeath;
        }


        private void Update()
        {
            if (PauseGameManagement.IsGamePaused)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.K)) // Debug
            {
                Health.Kill();
            }


            _combatManager.UpdateTick();
            _movementManager.UpdateTick();



        }

        private void FixedUpdate()
        {
            _combatManager.FixedUpdateTick();

            if (_movementManager.enabled) // Todo: confirm if I am relying on movementmanager.enabled now that the "enabled" tickbox is removed. Consider adding in other booleans either in this script or on the managers
            {
                _movementManager.FixedUpdateTick();
            }

            return;

            
        }

        private void SetUpCombatManager()
        {
            _combatManager = GetComponent<CombatManager>();
            _combatManager.Initialize();

        }

        private void SetUpMovementManager()
        {
            _movementManager = GetComponent<MovementManager>();
            _movementManager.Initialize();
        }

        private void DisableMovementOnDeath(Health health)
        {
            _movementManager.enabled = false;
        }
    }
}