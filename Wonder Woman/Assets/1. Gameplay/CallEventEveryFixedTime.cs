using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LupiLab.Core
{
    public class CallEventEveryFixedTime : MonoBehaviour
    {
        [SerializeField] protected float Period = 1f;

        [SerializeField] protected UnityEvent OnEvent;

        protected float _nextEventTime;

        private void Start()
        {
            SetNextEventTime();
        }

        private void FixedUpdate()
        {
            if (Time.time >= _nextEventTime)
            {
                CallEvent();
            }
        }

        private void CallEvent()
        {
            SetNextEventTime();
            OnEvent?.Invoke();
        }

        private void SetNextEventTime()
        {
            _nextEventTime = Time.time + Period;
        }

    }
}