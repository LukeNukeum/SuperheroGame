using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LupiLab.UI
{
    public class FloatPreferenceController : MonoBehaviour
    {
        [SerializeField] private SOFloat _scriptableObjectFloat;
        [SerializeField] private string _preferenceKey;
        private float _value;
        [SerializeField] private Slider _slider;

        public void SetValue (float value)
        {
            Debug.Log($"Setting value to {value}");
            _value = _scriptableObjectFloat.SetValue(value);
        }

        public void SavePrefsAndApply()
        {
            Debug.Log("Saving preferences");
            _scriptableObjectFloat.SetValue(_value);
            PlayerPrefs.SetFloat(_preferenceKey, _value);
            PlayerPrefs.Save();
        }
    }
}