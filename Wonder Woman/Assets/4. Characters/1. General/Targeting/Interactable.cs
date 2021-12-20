using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LupiLab.Targeting
{
    public class Interactable : Targetable
    {
        
        [SerializeField] private UnityEvent OnInteract;

        [SerializeField] private string _displayName = "Interactable";
        public string DisplayName => _displayName;
        [SerializeField] private string _promptMessage = "Interact";
        public string PromptMessage => _promptMessage;

        public void Interact(InteractionAgent agent)
        {
            Debug.Log($"{agent} interacted with {_displayName}");
            OnInteract?.Invoke();
        }


    }
}