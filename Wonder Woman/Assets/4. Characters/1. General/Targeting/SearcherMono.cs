using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Targeting {
    public class SearcherMono : MonoBehaviour, ISearcher
    {

        public Targetable Target { get; private set; }

        [SerializeField] private TargetFinder _targetFinder;
        [SerializeField] private Transform _lookPoint;

        public Vector3 Position { get { return _lookPoint.position; } }
        public Vector3 Direction { get { return transform.forward; } }
        public IEnumerable<Collider> Triggers { get { return new Collider[0]; } }
        public Targetable ThisTargetable { get; private set; }
        public bool HasTarget { get; protected set; }

        public bool GetTarget()
        {
            Target = _targetFinder.GetTarget(this);
            HasTarget = Target;
            return HasTarget;
        }

        public bool ValidateCurrentTarget()
        {
            if (!Target)
                return false;

            if (_targetFinder.Evaluate(this, Target))
                return true;
            else
            {
                Target = null;
                HasTarget = false;
                return false;
            }
        }

        public bool CanTargetCurrentTarget()
        {
            if (Target)
                return false;
            return _targetFinder.Evaluate(this, Target);
        }


        void Awake()
        {
            ThisTargetable = GetComponent<Targetable>();
        }



    }
}