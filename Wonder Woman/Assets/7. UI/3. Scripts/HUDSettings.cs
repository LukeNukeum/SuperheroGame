using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LupiLab.UI
{
    [CreateAssetMenu(fileName = "HUD Setting", menuName = "UI/HUD Setting")]
    public class HUDSettings : ScriptableObject
    {
        [field: SerializeField] public bool DoesShowHealthBar { get; private set; }
        [field: SerializeField] public bool DoesShowStaminaBar { get; private set; }
        [field: SerializeField] public bool DoesShowFlowBar { get; private set; }
    }
}