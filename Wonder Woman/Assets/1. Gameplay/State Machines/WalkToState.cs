using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LupiLab.Targeting;


namespace LupiLab.StateMachines.AI
{
    public class WalkToState : IState
    {

        private Agent _agent;
        private ISearcher _hunter;
        private float _scanDelay = 0.5f;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        private float _timeForNextScan = 0f;


        public WalkToState(Agent agent, ISearcher hunter, Animator animator, NavMeshAgent navMeshAgent)
        {
            _agent = agent;
            _hunter = hunter;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public void OnEnter()
        {
            //Debug.Log("Beginning Walk");
            _timeForNextScan = Time.fixedTime + Random.Range(0, _scanDelay);
            _animator?.SetFloat("WalkSpeed", 1);
            _navMeshAgent.enabled = true;
        }

        public void Tick()
        {
            if (_timeForNextScan <= Time.fixedTime)
            {
                _hunter?.ValidateCurrentTarget();
                if (_hunter.Target != null)
                {
                    _navMeshAgent.SetDestination(_hunter.Target.Position);
                }
                _timeForNextScan = Time.fixedTime + _scanDelay;
            }

            //StateMachine.SetState(new PatrolState(stateMachine: StateMachine));

        }

        public void OnExit()
        {
            //Debug.Log("Exiting Walk");
            _animator?.SetFloat("WalkSpeed", 0);
            _navMeshAgent.enabled = false;
            if (_navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.ResetPath();
            }
        }
    }
}