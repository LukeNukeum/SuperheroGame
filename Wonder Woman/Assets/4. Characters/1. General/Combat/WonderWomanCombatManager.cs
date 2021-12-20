using System;
using UnityEngine;
using LupiLab.StateMachines;

namespace LupiLab.Character
{
    [ExecuteAlways]
    public class WonderWomanCombatManager : CombatManager
    {

        // Weapon Swapping
        // Lasso
        // Power blast
        // Bow and arrow
        [SerializeField] private string _currentStateStatusString = ""; // Debug
        [SerializeField] private HeldItemsManager _heldItemsManager = new HeldItemsManager();
        [SerializeField] private CombatInput _input = new CombatInput(); // ToDo: May need to allow for other types of combat input systems, such as AI
        [SerializeField] private WonderWomanMovementManager _movementManager;

        private StateMachine _stateMachine;

        private NullCombatState _nullState;
        private BlockingCombatState _blockingState;
        private MeleeAttackCombatState _meleeAttackingState;
        private WeaponSwitchingState _weaponSwitchingState;
        private StaggerState _staggerState;

        public bool CanStartFlying
        {
            get
            {
                if (_stateMachine.CurrentState is NullCombatState || _stateMachine.CurrentState is BlockingCombatState || _stateMachine.CurrentState is WeaponSwitchingState)
                {
                    return true;
                }
                return false;
            }
        }


        public override void Initialize()
        {
            base.Initialize();
            SetUpStateMachine();
            SetUpHeldItemsManager();
            SetUpAnimationEventReceiver();
        }

        public override void UpdateTick()
        {
            _input.UpdateTick();

            //Debug:
            if (Input.GetKeyDown(KeyCode.G))
            {
                TryStartStaggering();
            }

        }

        public override void FixedUpdateTick()
        {
            base.FixedUpdateTick();

            _input.FixedUpdateTick();

            _stateMachine.Tick();
            _currentStateStatusString = _stateMachine.CurrentState.ToString(); // Debug

            _input.ResetFlags();
        }

        #region State machine
        protected void SetUpStateMachine()
        {
            _stateMachine = new StateMachine();

            var animator = GetComponentInChildren<Animator>();
            var blockingCharacterDamageable = GetComponent<BlockingCharacterDamageable>();

            _nullState = new NullCombatState();
            _blockingState = new BlockingCombatState(animator: animator, blockingCharacterDamageable: blockingCharacterDamageable);
            _meleeAttackingState = new MeleeAttackCombatState(animator: animator);
            _weaponSwitchingState = new WeaponSwitchingState(animator: animator);
            _staggerState = new StaggerState(animator: animator);

            _stateMachine.AddTransition(_nullState, _blockingState, HasBlockInput());
            //_stateMachine.AddTransition(_nullState, _meleeAttackingState, HasAttackInput());
            _input.AttackInputEvent += TryStartAttacking;

            _stateMachine.AddTransition(_nullState, _weaponSwitchingState, HasSwitchWeaponsInput());

            _stateMachine.AddTransition(_blockingState, _nullState, HasNoBlockInput());

            _stateMachine.AddTransition(_meleeAttackingState, _nullState, HasFinishedAttacking());

            _stateMachine.AddTransition(_weaponSwitchingState, _nullState, HasFinishedSwitchingWeapons());

            _staggerState.StaggerFinishedEvent += OnStaggerFinished;

            _stateMachine.SetState(_nullState);

        }

        //Blocking
        private Func<bool> HasBlockInput() => () => _input.HasBlockInput && _heldItemsManager.CurrentItemSet.CanBlock;
        private Func<bool> HasNoBlockInput() => () => !_input.HasBlockInput || !_heldItemsManager.CurrentItemSet.CanBlock;
        //Attacking
        //private Func<bool> HasAttackInput() => () => _input.HasAttackInput;
        private Func<bool> HasFinishedAttacking() => () => _meleeAttackingState.HasFinishedAttacking;
        //Switching held items
        private Func<bool> HasSwitchWeaponsInput() => () => _heldItemsManager.TargetItemSetIndex != _heldItemsManager.CurrentItemSetIndex;
        private Func<bool> HasFinishedSwitchingWeapons() => () => _weaponSwitchingState.HasFinishedSwitching;

        

        private void TryStartAttacking()
        {
            if(CanStartAttacking)
            {
                _stateMachine.SetState(_meleeAttackingState);
            }
        }

        private bool CanStartAttacking
        {
            get
            {
                if (!(_stateMachine.CurrentState is NullCombatState))
                {
                    return false;
                }
                if (_movementManager)
                {
                    if (!_movementManager.CanStartMeleeAttack)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public void TryStartStaggering()
        {
            _stateMachine.SetState(_staggerState);
        }

        public void OnStaggerFinished()
        {
            _stateMachine.SetState(_nullState);
        }

        #endregion




        private void SetUpAnimationEventReceiver()
        {
            AnimationEventReceiver animationEventReceiver = GetComponentInChildren<AnimationEventReceiver>();
            if (animationEventReceiver)
            {
                animationEventReceiver.WeaponSwitchEvent += _heldItemsManager.OnWeaponSwitch;


            }
        }
        

        // TODO Add in a way for state changes to be made by method calls. I.e. SwitchWeapon(1) sets some fields and immediately changes to the switch weapon state. This would need to have some kind of "allowable transition" list so that state transitions were only made from the correct states
        // ToDO Link the movement states to the combat states so that the correct movement state is active for the active combat state. Also put movement state requirements on relevant combat states, i.e. melee attack when grounded

        private void SetUpHeldItemsManager()
        {
            _input.HeldItemToggleInputEvent += _heldItemsManager.OnStartHeldItemToggle;
            _input.SwitchToHeldItemInputEvent += _heldItemsManager.OnStartSwitchToHeldItem;
            _heldItemsManager.Initialize();
        }


        private void OnGUI()
        {
            GUILayout.Label($"Combat State: {_stateMachine.CurrentState}");
        }
    }
}