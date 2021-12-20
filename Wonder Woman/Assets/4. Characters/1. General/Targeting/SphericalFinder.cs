using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace LupiLab.Targeting
{
    [CreateAssetMenu(fileName = "Spherical Finder", menuName = "Targeting/Finders/Spherical Finder")]
    public class SphericalFinder : TargetFinder
    {
        [SerializeField] private TargetableList _targetableList;
        [SerializeField] private float _maxRange = 10f;
        [SerializeField] private bool _doesExcludeSelf = true;
        [SerializeField] private Faction[] _targetFactions;
        [SerializeField] private bool _doRaycast = true;
        [SerializeField] private LayerMask _raycastLayerMask;

        public override Targetable GetTarget(ISearcher hunter)
        {
            if(_targetableList == null)
            {
                return null;
            }
            List<Targetable> validTargets;
            validTargets = _targetableList.Targetables                  // Note: Linq queries are inefficient, this could be improved in the future.
                .Where(targetable => Evaluate(hunter, targetable))
                .OrderBy(targetable => Vector3.SqrMagnitude(hunter.Position-targetable.Position))
                .ToList();
            if(validTargets.Count > 0)
            {
                return (validTargets[0]);
            }
            return null;
        }

        public override bool Evaluate(ISearcher hunter, Targetable targetable)
        {
            if (!targetable.isActiveAndEnabled)
                return false;

            if (_doesExcludeSelf && hunter.ThisTargetable == targetable)
                return false;

            if (!_targetFactions.Contains(targetable.Faction))
                return false;

            float distance = Vector3.Distance(hunter.Position, targetable.Position);
            if (distance > _maxRange)
                return false;

            if (IsViewObstructed(hunterPosition: hunter.Position, targetPosition: targetable.Position, distance: distance))
                return false;

            return true;
        }

        private bool IsViewObstructed(Vector3 hunterPosition, Vector3 targetPosition, float distance)
        {
            if (!_doRaycast)
                return false;

            Debug.DrawLine(hunterPosition, targetPosition, Color.red, 2);
            return Physics.Raycast(origin: hunterPosition, direction: targetPosition - hunterPosition, maxDistance: distance, layerMask: _raycastLayerMask);
        }
    }
}