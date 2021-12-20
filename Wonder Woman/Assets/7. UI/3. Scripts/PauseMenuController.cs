using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Core;

namespace LupiLab.UI
{
    public class PauseMenuController : MonoBehaviour
    {

        private CursorLocker _cursorLocker;
        [SerializeField] private Menu _pauseMenu;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!PauseGameManagement.IsGamePaused)
                {
                    OpenPauseMenu();
                    
                }
            }
        }


        public void OpenPauseMenu()
        {
            StartCoroutine(OpenPauseMenuRoutine());
        }

        private IEnumerator OpenPauseMenuRoutine()
        {
            yield return new WaitForEndOfFrame();
            PauseGameManagement.PauseGame();
            _cursorLocker?.UnlockCursor();
            _pauseMenu?.Open();
        }

        public void ClosePauseMenu()
        {
            StartCoroutine(ClosePauseMenuRoutine());
        }

        private IEnumerator ClosePauseMenuRoutine()
        {
            yield return new WaitForEndOfFrame();
            PauseGameManagement.UnpauseGame();
            _cursorLocker?.LockCursor();
            _pauseMenu?.Close();
        }
    }
}