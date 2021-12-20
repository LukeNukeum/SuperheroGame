using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Core;

namespace LupiLab.LevelManagement
{
    public class LevelSwitchController : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;

        public static event Action BeginUnloadAllScenesEvent;


        public void SwitchToLevel(int sceneBuildIndex)
        {
            StartCoroutine(SwitchToLevelRoutine(sceneBuildIndex));
        }
        public void SwitchToLevel(string sceneName)
        {
            StartCoroutine(SwitchToLevelRoutine(sceneName));
        }

        private IEnumerator SwitchToLevelRoutine(int sceneBuildIndex)
        {
            BeginUnloadAllScenesEvent?.Invoke();
            PauseGameManagement.UnpauseGame();

            yield return new WaitForEndOfFrame();
            
            _levelManager.LoadScene(sceneBuildIndex);
            yield break;
        }
        private IEnumerator SwitchToLevelRoutine(string sceneName)
        {
            BeginUnloadAllScenesEvent?.Invoke();
            PauseGameManagement.UnpauseGame();

            yield return new WaitForEndOfFrame();
            _levelManager.LoadScene(sceneName);
            yield break;
        }




    }
}