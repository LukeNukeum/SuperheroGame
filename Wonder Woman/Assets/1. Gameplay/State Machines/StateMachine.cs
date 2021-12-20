using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LupiLab.StateMachines
{
    public class StateMachine
    {
        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();
        private static List<Transition> EmptyTransitions = new List<Transition>(capacity: 0);

        public IState CurrentState { get; private set; }

        public void Tick()
        {
            var transition = GetTransition();
            if(transition!= null)
            {
                SetState(transition.To);
            }

            CurrentState?.Tick();
        }

        public void SetState(IState state)
        {
            if(state == CurrentState)
                return;

            CurrentState?.OnExit();
            CurrentState = state;

            _transitions.TryGetValue(CurrentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            CurrentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if(_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState to, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(to, predicate));
        }

        private class Transition
        {
            public IState To { get; }
            public Func<bool> Condition { get; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        private Transition GetTransition()
        {
            foreach(var transition in _anyTransitions)
            {
                if (transition.Condition())
                    return transition;
            }
            foreach(var transition in _currentTransitions)
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }
            return null;
        }

    }
}