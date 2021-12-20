using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Core
{
    public class PauseGameManagement
    {
        public static event Action GamePauseEvent;
        public static event Action GameUnpauseEvent;

        public static bool IsGamePaused = false;

        public static void PauseGame()
        {
            if (IsGamePaused) return;
            IsGamePaused = true;
            GameTime.PauseTime();
            GamePauseEvent?.Invoke();
        }

        public static void UnpauseGame()
        {
            if (!IsGamePaused) return;
            IsGamePaused = false;
            GameTime.UnpauseTime(1);
            GameUnpauseEvent?.Invoke();
        }
    }
}