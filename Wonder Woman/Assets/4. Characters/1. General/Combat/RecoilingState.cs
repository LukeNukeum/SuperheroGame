using UnityEngine;

namespace LupiLab.Character
{
    public class RecoilingState : CombatState
    {

        protected Animator _animator;

        public RecoilingState(Animator animator)
        {
            _animator = animator;
        }



        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
        }

        public override void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}