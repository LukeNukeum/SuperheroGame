using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Targeting;

namespace LupiLab.StateMachines.AI
{
    public class AttackState : IState
    {

        public bool IsAttackInCooldown { get { return _timeForNextAttack > Time.fixedTime; } }

        private Agent _agent;
        private ISearcher _hunter;
        private Animator _animator;
        private float _attackDelay = 3f;


        private float _timeForNextAttack = 0f;
        

        public AttackState(Agent agent, ISearcher hunter, Animator animator)
        {
            _agent = agent;
            _hunter = hunter;
            _animator = animator;
        }

        public AttackState(Agent agent, ISearcher hunter, Animator animator, float attackDelay)
        {
            _agent = agent;
            _hunter = hunter;
            _animator = animator;
            _attackDelay = attackDelay;
        }


        public void OnEnter()
        {
            //Debug.Log("Beginning Patrol");
            _timeForNextAttack = Time.fixedTime;
        }

        public void Tick()
        {
            
            if(_timeForNextAttack <= Time.fixedTime)
            {
                _hunter.ValidateCurrentTarget();
                if(_hunter.Target != null)
                {
                    Attack();
                }

                _timeForNextAttack = Time.fixedTime + _attackDelay;
                //Debug.Log($"Fixed time: {Time.fixedTime}, Time for next attack{_timeForNextAttack}");
            }
            //StateMachine.SetState(new WalkToState(StateMachine));

            
        }

        public void OnExit()
        {
            //Debug.Log("Exiting Patrol");
        }

        public void Attack()
        {
            //Debug.Log($"{_agent.gameObject.name} attacks!");
            _animator.SetTrigger("Attack");
        }

    }
}