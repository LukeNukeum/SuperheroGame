using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    [System.Serializable]
    public class HeldItemsManager
    {
        
        [field: SerializeField] public int CurrentItemSetIndex { get; private set; } = 0;
        [field: SerializeField] public int TargetItemSetIndex { get; private set; } = 0;

        [SerializeField] private HeldItemSet _allHeldItems;
        [SerializeField] private HeldItemSet[] _heldItemSets;

        public HeldItemSet CurrentItemSet => _heldItemSets[CurrentItemSetIndex];

        public void Initialize()
        {
            CurrentItemSetIndex = Mathf.Clamp(CurrentItemSetIndex, 0, _heldItemSets.Length);
            TargetItemSetIndex = CurrentItemSetIndex;
            _allHeldItems.Disable();
            _heldItemSets[CurrentItemSetIndex].Enable();
        }

        public void SetActiveItemSet(int itemSetIndex)
        {
            if(itemSetIndex>=0 && itemSetIndex < _heldItemSets.Length)
            {
                if(itemSetIndex != CurrentItemSetIndex)
                {
                    _allHeldItems.Disable();
                    _heldItemSets[itemSetIndex].Enable();
                    CurrentItemSetIndex = itemSetIndex;
                }
            }
        }

        public void OnWeaponSwitch()
        {
            if(TargetItemSetIndex != CurrentItemSetIndex)
            {
                SetActiveItemSet(TargetItemSetIndex);
                TargetItemSetIndex = CurrentItemSetIndex;
            }

        }

        public void OnStartSwitchToHeldItem(int targetIndex)
        {
            if(targetIndex >= 0 && targetIndex < _heldItemSets.Length)
            {
                TargetItemSetIndex = targetIndex;
            }
        }

        public void OnStartHeldItemToggle()
        {
            if(CurrentItemSetIndex == 0)
            {
                TargetItemSetIndex = 1;
            }
            else
            {
                TargetItemSetIndex = 0;
            }
        }

        [System.Serializable]
        public class HeldItemSet
        {
            [SerializeField] bool _canBlock = false;
            [SerializeField] GameObject[] _heldItems;
            [SerializeField] string _changeWeaponAnimationName = ""; //Todo - Select the weapon switch animation appropriate for the weapon. E.g. switching from sword to bow - Sheath sword, Draw bow
            [SerializeField] RuntimeAnimatorController _runtimeAnimatorController;

            public bool CanBlock => _canBlock;
            

            public void Enable()
            {
                foreach (GameObject weapon in _heldItems)
                {
                    weapon.SetActive(true);
                    //animator.Play(_changeWeaponAnimationName, 1, 0.1f);
                    //animator.runtimeAnimatorController = RuntimeAnimatorController;
                }
            }
            public void Disable()
            {
                //Debug.Log($"Disabling {_heldItems.Length} weapons");
                foreach (GameObject weapon in _heldItems)
                {
                    weapon.SetActive(false);
                }
            }
        }

    }
}