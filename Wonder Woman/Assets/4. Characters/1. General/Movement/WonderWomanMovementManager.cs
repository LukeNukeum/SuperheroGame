using UnityEngine;
using LupiLab.Camera;
using LupiLab.Character.Motion;
using LupiLab.StateMachines;

namespace LupiLab.Character {
    public class WonderWomanMovementManager :MovementManager
    {

        [SerializeField] private string _currentStateStatusString = "";

        [SerializeField] protected FlyingMovement.FlySettings _flySettings;
        [SerializeField] protected SOFloat _inputDoublePressTime;
        [SerializeField] protected CharacterWalkMotionHandler.GroundScanSettings _motionHandlerGroundScanSettings;
        [SerializeField] protected WonderWomanCombatManager _combatManager;

        public override bool IsGrounded
        {
            get
            {
                if (_stateMachine.CurrentState is FlyingMovement)
                {
                    return false;
                }

                return (_walkMotionHandler.IsGrounded);
            }
        }

        // Input Handlers
        protected WalkingMovementInput _walkingInput;
        protected FlyingMovementInput _flyingInput;

        // State Machine
        protected StateMachine _stateMachine;

        // Movement States
        protected WalkingMovement _walkingMovement;
        protected WalkingStrafeMovement _walkingStrafeMovement;
        protected FlyingMovement _flyingMovement;

        // Motion Handlers
        protected CharacterWalkMotionHandler _walkMotionHandler;
        protected CharacterControllerPhysics _characterControllerPhysics;



        public bool CanStartMeleeAttack
        {
            get
            {
                if (_stateMachine.CurrentState is WalkingMovement || _stateMachine.CurrentState is WalkingStrafeMovement)
                {
                    return true;
                }
                return false;
            }
        }


        public override void Initialize()
        {
            //var rigidbody = GetComponent<Rigidbody>();
            var characterController = GetComponent<CharacterController>();
            var animator = GetComponentInChildren<Animator>();
            var animationMotionReceiver = animator.GetComponent<AnimationMotionReceiver>();

            _characterControllerPhysics = new CharacterControllerPhysics();

            _walkingInput = new WalkingMovementPlayerInput(cameraRig: ThirdPersonCamera.MainCameraRig, _inputDoublePressTime);
            _flyingInput = new FlyingMovementPlayerInput(cameraRig: ThirdPersonCamera.MainCameraRig);

            _walkMotionHandler = new CharacterControllerWalkMotionHandler(characterController, _motionHandlerGroundScanSettings, _characterControllerPhysics);


            SetUpStateMachine(characterController, animator, animationMotionReceiver);

            animationMotionReceiver.MotionHandler = _walkMotionHandler;
            _stateMachine.SetState(_walkingMovement);
        }

        public override void UpdateTick()
        {
            _walkingInput.UpdateTick();
        }

        public override void FixedUpdateTick()
        {
            _walkingInput.FixedUpdateTick();

            _stateMachine.Tick();

            base.FixedUpdateTick();
            _currentStateStatusString = _stateMachine.CurrentState.ToString();

            _walkingInput.ResetFlags();
        }

        // Todo: Refactor movement statemachine to acheive the following:
        //         Use the state machine class
        //         Interact with the combat state machine
        //         Handle direct methods to switch to states that include validity checks - the direct acting methods will be implemented in this class, including requisite checking

        protected void SetUpStateMachine(CharacterController characterController, Animator animator, AnimationMotionReceiver animationMotionReceiver)
        {
            _stateMachine = new StateMachine();

            

            _walkingMovement = new WalkingMovement(transform, _walkingInput, animator);
            _walkingStrafeMovement = new WalkingStrafeMovement(transform, _walkingInput, animator, ThirdPersonCamera.MainCameraRig);
            _flyingMovement = new FlyingMovement(transform, GetComponent<Rigidbody>(), _flyingInput, animator, _flySettings, animationMotionReceiver, characterController, _characterControllerPhysics);


            _walkingInput.ToggleFlyingInputEvent += TryToggleFlying;
        }

        private void TryToggleFlying()
        {
            if(_stateMachine.CurrentState is FlyingMovement)
            {
                if (CanStopFlying)
                {
                    _stateMachine.SetState(_walkingMovement);
                }
            }
            else
            {
                if (CanStartFlying)
                {
                    _stateMachine.SetState(_flyingMovement);
                }
            }
            
        }

        private bool CanStartFlying
        {
            get
            {
                if (_combatManager)
                {
                    if (!_combatManager.CanStartFlying)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private bool CanStopFlying
        {
            get
            {
                return true;
            }
        }


        //private void DoStateSwitching() 
        //{
        //    if (_walkingInput.DoToggleFlying)
        //    {
        //        Debug.Log($"Toggling flying from {CurrentMovement}");
        //        if(CurrentMovement == _flyingMovement)
        //        {
        //            if (_walkingInput.IsStrafing) SetActiveMovement(_walkingStrafeMovement);
        //            else SetActiveMovement(_walkingMovement);
        //        }
        //        else
        //        {
        //            SetActiveMovement(_flyingMovement);
        //        }
        //        return;
        //    }

        //    if (CurrentMovement != _flyingMovement)
        //    {
        //        if (_walkingInput.IsStrafing)
        //        {
        //            SetActiveMovement(_walkingStrafeMovement);
        //        }
        //        else
        //        {
        //            SetActiveMovement(_walkingMovement);
        //        }
        //    }
        //}

        //private void SetActiveMovement(Movement movement)
        //{
        //    if (movement == CurrentMovement) return;

        //    CurrentMovement.OnExit();
        //    CurrentMovement = movement;
        //    CurrentMovement.OnEnter();
        //}


        private void OnGUI()
        {
            GUILayout.Label($"Grounded: {IsGrounded}");
        }
    }
}