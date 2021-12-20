using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.UI;

namespace LupiLab.Targeting
{
    public class InteractionAgent : MonoBehaviour
    {

        

        [SerializeField] private ISearcher _searcher;
        [SerializeField] private UIInteractionTooltip _interactionTooltip;
        [SerializeField] private Searcher.SearcherSettings _searcherSettings;

        public Interactable CurrentTarget => _currentTarget;


        public event Action<Interactable> TargetChangedEvent;
        public event Action TargetLostEvent;

        protected bool _doInteractionOnNextFixedUpdate = false;
        protected Interactable _currentTarget = null;

        private void Start()
        {
            _searcher = new Searcher(_searcherSettings);

            _interactionTooltip = FindObjectOfType<UIInteractionTooltip>();
            if (_interactionTooltip != null)
            {
                _interactionTooltip.Bind(this);
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Interact"))
            {
                _doInteractionOnNextFixedUpdate = true;
            }
        }

        private void FixedUpdate()
        {
            Interactable newTargetInteractable = GetInteractableTarget();
            if(newTargetInteractable != _currentTarget)
            {
                if(newTargetInteractable == null)
                {
                    TargetLostEvent?.Invoke();
                }
                else
                {
                    TargetChangedEvent?.Invoke(newTargetInteractable);
                }
                _currentTarget = newTargetInteractable;
            }

            if (_doInteractionOnNextFixedUpdate)
                if (_currentTarget != null)
                    Interact(_currentTarget);
            _doInteractionOnNextFixedUpdate = false;
        }

        private Interactable GetInteractableTarget()
        {
            if (_searcher == null) return null;

            if (_searcher.GetTarget())
            {
                return _searcher.Target.GetComponent<Interactable>();
            }

            return null;
        }

        void Interact(Interactable target)
        {
            target.Interact(this);
        }
    }
}