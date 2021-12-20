using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Character
{
    public abstract class Character : MonoBehaviour
    {
        const int _propLayerIndex = 8;

        [SerializeField] private CharacterSettings _settings;
        [SerializeField] private MonoBehaviour[] _monoBehavioursToDisableOnDeath;
        

        public virtual Health Health { get; private set; }
        public virtual CharacterStat Stamina { get; private set; }
        public virtual CharacterStat Flow { get; private set; }

        protected RecoilOnTakeDamage _recoilOnTakeDamage;
        protected PlayAnimationOnDeath _playAnimationOnDeath;

        protected MovementManager _movementManager;

        protected virtual void Awake()
        {
            Health = new Health(_settings.MaxHealth, _settings.MaxHealth);
            Stamina = new CharacterStat(_settings.MaxStamina, _settings.MaxStamina);
            Flow = new CharacterStat(_settings.MaxFlow, 0);

            Animator animator = GetComponentInChildren<Animator>();
            _movementManager = GetComponent<MovementManager>();
            _recoilOnTakeDamage = new RecoilOnTakeDamage(Health, animator, _movementManager, _settings.RecoilModes);
            _playAnimationOnDeath = new PlayAnimationOnDeath(Health, animator);

            Health.DeathEvent += OnDeath;

            SetUpDamageableBehaviour();



        }

        [System.Serializable]
        internal class CharacterSettings
        {
            public float MaxHealth = 100;
            public float MaxStamina = 100;
            public float MaxFlow = 3;

            public RecoilMode[] RecoilModes;
        }

        protected virtual void OnDeath(Health health)
        {
            Destroy(gameObject, 2f);
            gameObject.layer = _propLayerIndex;
            foreach(MonoBehaviour behaviour in _monoBehavioursToDisableOnDeath)
            {
                behaviour.enabled = false;
            }
        }

        protected virtual void SetUpDamageableBehaviour()
        {
            Damageable damageable = GetComponent<Damageable>();
            damageable?.SetHealthStatTarget(Health);
            if (damageable is BlockingCharacterDamageable blockingDamageable)
            {
                blockingDamageable.SetStaminaStatTarget(Stamina);
                blockingDamageable.SetTransformTarget(transform);
            }
        }
    }
}