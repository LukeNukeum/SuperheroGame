using UnityEngine;
using UnityEngine.UI;
using LupiLab.Character;

namespace LupiLab.UI
{
    public class UIHealthBar : MonoBehaviour
    {

        [SerializeField] private Health _targetHealth;
        [SerializeField] private Image _healthBarFill;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _deathColor;

        private void Awake()
        {
            _baseColor = _healthBarFill.color;
        }

        private void OnEnable()
        {
            if (_targetHealth != null)
            {
                Bind(_targetHealth);
            }
        }

        private void OnDisable()
        {
            Unbind();
        }

        public void SetTargetHealth(GameObject targetGameObject)
        {
            SetTargetHealth(targetGameObject.GetComponent<Health>());
        }

        public void SetTargetHealth(Health health)
        {
            Debug.Log($"binding to {health}");
            if(_targetHealth != null)
            {
                if (_targetHealth == health)
                    return;
                else
                {
                    if (isActiveAndEnabled)
                    {
                        Unbind();
                        if(health != null)
                            Bind(health);
                    }
                    _targetHealth = health;
                    return;
                }
            }
            else
            {
                _targetHealth = health;
                if (isActiveAndEnabled && health != null)
                {
                    Bind(health);
                }
                return;
            }
        }

        void Bind(Health health)
        {
            if (health.IsDead)
            {
                _healthBarFill.fillAmount = 1;
                _healthBarFill.color = _deathColor;
            }
            else
            {
                SetHealthBarFill(health);
                _healthBarFill.color = _baseColor;
            }
            health.TakeDamageEvent += OnReceiveDamage;
            health.DeathEvent += OnDeath;
        }

        void Unbind()
        {
            if (_targetHealth == null) return;

            _targetHealth.TakeDamageEvent -= OnReceiveDamage;
            _targetHealth.DeathEvent -= OnDeath;
        }

        void OnReceiveDamage(Health health, Damage damage)
        {
            SetHealthBarFill(health);
        }

        void OnDeath(Health health)
        {
            _healthBarFill.fillAmount = 1;
            _healthBarFill.color = _deathColor;
        }

        private void SetHealthBarFill(Health health)
        {
            _healthBarFill.fillAmount = _targetHealth.Value / _targetHealth.MaxValue;
        }
    }
}