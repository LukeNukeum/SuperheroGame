using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LupiLab.StateMachines;
using LupiLab.Targeting;

namespace LupiLab.StateMachines.AI
{
    public class Agent : MonoBehaviour
    {

        [SerializeField] private float _attackingRange = 2f;
        [SerializeField] private float _attackDelay = 3f;

        private StateMachine _stateMachine;
        private SearcherMono _hunter;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        private AttackState _attack;

        private void Awake()
        {
            _hunter = GetComponent<SearcherMono>();
            _stateMachine = new StateMachine();
            _animator = GetComponentInChildren<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            var patrol = new PatrolState(agent: this, hunter: _hunter, animator: _animator);
            var walkTo = new WalkToState(agent: this, hunter: _hunter, animator: _animator, navMeshAgent: _navMeshAgent);
            _attack = new AttackState(agent: this, hunter: _hunter, animator: _animator, _attackDelay);

            _stateMachine.AddTransition(patrol, walkTo, HasTarget());
            _stateMachine.AddTransition(walkTo, patrol, HasNoTarget());
            _stateMachine.AddTransition(walkTo, _attack, IsInAttackingRange());
            _stateMachine.AddTransition(_attack, patrol, HasNoTarget());
            _stateMachine.AddTransition(_attack, walkTo, IsNotInAttackingRangeAndOutOfCooldown());


            _stateMachine.SetState(patrol);

        }



        private void FixedUpdate()
        {
            _stateMachine.Tick();
        }



        private Func<bool> HasTarget() => () => _hunter.Target;
        private Func<bool> HasNoTarget() => () => !_hunter.Target;
        private Func<bool> IsInAttackingRange() => () => Vector3.SqrMagnitude(_hunter.Target.Position - _hunter.Position) <= _attackingRange * _attackingRange;
        private Func<bool> IsNotInAttackingRangeAndOutOfCooldown() => () => _attack.IsAttackInCooldown && (Vector3.SqrMagnitude(_hunter.Target.Position - _hunter.Position) > _attackingRange * _attackingRange);


    }
}