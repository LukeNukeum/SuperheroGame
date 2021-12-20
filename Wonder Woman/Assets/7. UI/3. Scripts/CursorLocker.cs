using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.UI
{
    [AddComponentMenu("LupiLab/UI/Cursor Locker")]
    [DisallowMultipleComponent]
    public class CursorLocker : MonoBehaviour
    {
        



        public void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UnlockCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}