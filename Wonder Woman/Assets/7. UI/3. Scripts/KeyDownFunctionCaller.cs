using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace LupiLab.UI
{
    [AddComponentMenu("LupiLab/UI/Key Down Function Caller")]
    public class KeyDownFunctionCaller : MonoBehaviour
    {
        [SerializeField] private KeyCode _key;

        [SerializeField] private UnityEvent OnKeyDown;

        private void Update()
        {
            if (Input.GetKeyDown(_key))
            {
                OnKeyDown?.Invoke();
            }
        }


    }
}