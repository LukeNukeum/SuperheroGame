using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab
{
    public class PlayerPrefsLoader : MonoBehaviour
    {
        [SerializeField] private PlayerPreferenceFloat[] floatPreferences;
        [SerializeField] private PlayerPreferenceInt[] intPreferences;
        [SerializeField] private PlayerPreferenceString[] stringPreferences;

        private void Start()
        {
            foreach(PlayerPreferenceFloat pref in floatPreferences)
                pref.LoadPreference();
            foreach (PlayerPreferenceInt pref in intPreferences)
                pref.LoadPreference();
            foreach (PlayerPreferenceString pref in stringPreferences)
                pref.LoadPreference();
        }

        [System.Serializable]
        private class PlayerPreferenceFloat
        {
            [SerializeField] private SOFloat _sOFloat;
            [SerializeField] private string _key;

            public void LoadPreference()
            {
                if (PlayerPrefs.HasKey(_key))
                {
                    //Debug.Log($"Setting {_sOFloat} to {_key}: {PlayerPrefs.GetFloat(_key)}");
                    _sOFloat.SetValue (PlayerPrefs.GetFloat(_key));
                }
            }
        }
        [System.Serializable]
        private class PlayerPreferenceInt
        {
            [SerializeField] private SOInt _sOInt;
            [SerializeField] private string _key;

            public void LoadPreference()
            {
                if (PlayerPrefs.HasKey(_key))
                {
                    _sOInt.SetValue(PlayerPrefs.GetInt(_key));
                }
            }
        }
        [System.Serializable]
        private class PlayerPreferenceString
        {
            [SerializeField] private SOString _sOString;
            [SerializeField] private string _key;

            public void LoadPreference()
            {
                if (PlayerPrefs.HasKey(_key))
                {
                    _sOString.SetValue(PlayerPrefs.GetString(_key));
                }
            }
        }
    }
}