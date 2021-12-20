using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LupiLab.LevelManagement {
    public class LevelTransitionScreen : MonoBehaviour
    {

        //Note, this script doesn't currently works. The transition screen doesn't appear before the scenes start changing. Consider using a coroutine.
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private GameObject _transitionScreen;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            LevelSwitchController.BeginUnloadAllScenesEvent += ShowTransitionScreen;
            _levelManager.FinishLoadSceneEvent += HideTransitionScreen;
        }

        private void OnDisable()
        {
            LevelSwitchController.BeginUnloadAllScenesEvent -= ShowTransitionScreen;
            _levelManager.FinishLoadSceneEvent -= HideTransitionScreen;
        }

        private void ShowTransitionScreen()
        {
            Debug.Log("Showing transition screen");
            _transitionScreen.SetActive(true);
        }

        private void HideTransitionScreen()
        {
            Debug.Log("Hiding transition screen");
            _transitionScreen.SetActive(false);
        }

    }
}