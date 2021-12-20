using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Targeting
{
    public class Searcher : ISearcher
    {
        public Targetable Target { get; private set; }


        public Vector3 Position { get { return _lookPoint.position; } }
        public Vector3 Direction { get { return _lookPoint.forward; } }
        public IEnumerable<Collider> Triggers { get { return new Collider[0]; } }
        public Targetable ThisTargetable { get; protected set; }
        public bool HasTarget { get; protected set; }

        protected TargetFinder _targetFinder;
        protected Transform _lookPoint;

        public Searcher(SearcherSettings settings)
        {
            _targetFinder = settings.TargetFinder;
            _lookPoint = settings.LookPoint;
        }

        public Searcher(SearcherSettings settings, Targetable thisTargetable)
        {
            _targetFinder = settings.TargetFinder;
            _lookPoint = settings.LookPoint;
            ThisTargetable = thisTargetable;
        }


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

        [System.Serializable]
        public class SearcherSettings
        {
            public TargetFinder TargetFinder;
            public Transform LookPoint;
        }
    }
}