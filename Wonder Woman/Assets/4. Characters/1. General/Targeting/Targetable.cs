using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Targeting
{
    public class Targetable : MonoBehaviour
    {
        [SerializeField] private Transform _focalPoint;
        [SerializeField] private Faction _faction;
        [SerializeField] private TargetableList _targetableList;

        public Vector3 Position => _focalPoint.position;
        public Faction Faction => _faction;

        protected virtual void OnEnable()
        {
            _targetableList?.Add(this);
        }

        protected virtual void OnDisable()
        {
            _targetableList?.Remove(this);
        }
    }
}