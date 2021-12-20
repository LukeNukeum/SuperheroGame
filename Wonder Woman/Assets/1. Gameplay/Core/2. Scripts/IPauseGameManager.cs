using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseGameManager
{
    void PauseGame();
    void UnpauseGame();
    bool IsGamePaused { get; }
}
