using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LupiLab.LevelManagement
{
    public class OpenMainMenuAsDefault : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _mainMenuSceneIndex = 2;
        [SerializeField] private LevelManager _levelManager;

        void Start()
        {
            if(LevelManager.ActiveScenes.Length == 1)
                _levelManager.LoadScene(_mainMenuSceneIndex);
        }
    }
}