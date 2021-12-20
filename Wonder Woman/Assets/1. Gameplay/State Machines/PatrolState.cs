using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Targeting;

namespace LupiLab.StateMachines.AI
{
    public class PatrolState : IState
    {

        private Agent _agent;
        private ISearcher _hunter;
        private Animator _animator;
        private float _scanDelay = 0.5f;


        private float _timeForNextScan = 0f;
        

        public PatrolState(Agent agent, ISearcher hunter, Animator animator)
        {
            _agent = agent;
            _hunter = hunter;
            _animator = animator;
        }

        public PatrolState(Agent agent, ISearcher hunter, Animator animator, float scanDelay)
        {
            _agent = agent;
            _hunter = hunter;
            _animator = animator;
            _scanDelay = scanDelay;
        }


        public void OnEnter()
        {
            //Debug.Log("Beginning Patrol");
            _timeForNextScan = Time.fixedTime + Random.Range(0, _scanDelay);
        }

        public void Tick()
        {
            
            if(_timeForNextScan <= Time.fixedTime)
            {
                //Debug.Log("Scanning for new target!");
                _hunter?.GetTarget();
                _timeForNextScan = Time.fixedTime + _scanDelay;
            }
            //StateMachine.SetState(new WalkToState(StateMachine));




            







        }

        public void OnExit()
        {
            //Debug.Log("Exiting Patrol");
        }
    }
}