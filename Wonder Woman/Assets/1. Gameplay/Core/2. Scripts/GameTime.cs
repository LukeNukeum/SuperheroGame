using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Core
{
    public class GameTime
    {
        public static void PauseTime()
        {
            Time.timeScale = 0;
        }

        public static void UnpauseTime(float timeScale)
        {
            Time.timeScale = timeScale;
        }
    }
}