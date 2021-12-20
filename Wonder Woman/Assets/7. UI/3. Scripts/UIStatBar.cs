using UnityEngine;
using UnityEngine.UI;
using LupiLab.Character;

namespace LupiLab.UI
{
    public class UIStatBar : MonoBehaviour
    {

        private CharacterStat _targetStat;
        [SerializeField] private Image _fillImage;

        void OnTargetValueChanged(CharacterStat stat, float oldValue, float newValue)
        {
            _fillImage.fillAmount = newValue / _targetStat.MaxValue;
        }

        private void OnEnable()
        {
            if (_targetStat != null)
            {
                Bind(_targetStat);
            }
        }

        public void SetTarget(CharacterStat newTargetStat) 
        {
            if (_targetStat != null)
            {
                if (_targetStat == newTargetStat)
                    return;
                else
                {
                    if (isActiveAndEnabled)
                    {
                        Unbind();
                        if (newTargetStat != null)
                            Bind(newTargetStat);
                    }
                    _targetStat = newTargetStat;
                    return;
                }
            }
            else
            {
                _targetStat = newTargetStat;
                if (isActiveAndEnabled && newTargetStat != null)
                {
                    Bind(newTargetStat);
                }
                return;
            }
        }

        void Bind(CharacterStat stat)
        {
            stat.ValueChangedEvent += OnTargetValueChanged;
            _fillImage.fillAmount = stat.Value / stat.MaxValue;
        }

        private void OnDisable()
        {
            Unbind();
        }

        void Unbind()
        {
            if (_targetStat == null) return;
            _targetStat.ValueChangedEvent-= OnTargetValueChanged;
        }

    }
}