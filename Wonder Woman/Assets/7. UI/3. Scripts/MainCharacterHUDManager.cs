using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Character;

namespace LupiLab.UI
{
    public class MainCharacterHUDManager : MonoBehaviour
    {
        public static MainCharacterHUDManager Instance { get; private set; }

        [SerializeField] private UIStatBar healthBar;
        [SerializeField] private UIStatBar staminaBar;
        [SerializeField] private UIStatBar flowBar;

        public void SetTarget(Character.Character character, HUDSettings hudSettings)
        {
            SetupUIBar(healthBar, character.Health, hudSettings.DoesShowHealthBar);
            SetupUIBar(staminaBar, character.Stamina, hudSettings.DoesShowStaminaBar);
            SetupUIBar(flowBar, character.Flow, hudSettings.DoesShowFlowBar);
        }

        public void SetTarget(Character.Character character)
        {
            healthBar.SetTarget(character.Health);
            healthBar.gameObject.SetActive(character.Health != null);

            staminaBar.SetTarget(character.Stamina);
            staminaBar.gameObject.SetActive(character.Stamina != null);

            flowBar.SetTarget(character.Flow);
            flowBar.gameObject.SetActive(character.Flow != null);

        }

        private void SetupUIBar(UIStatBar statBar, CharacterStat stat, bool doesShow)
        {
            statBar.gameObject.SetActive(doesShow);
            if (doesShow)
            {
                statBar.SetTarget(stat);
            }
            else
            {
                statBar.SetTarget(null);
            }
        }

        private void OnEnable()
        {
            if (Instance != null)
            {
                Debug.LogError("A static instance for MainCharacterHUDManager already exists!");
                Destroy(this);
                return;
            }
            Instance = this;
        }

        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

    }




}