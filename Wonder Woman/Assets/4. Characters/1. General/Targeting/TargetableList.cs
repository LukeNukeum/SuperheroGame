using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Targeting
{
    [CreateAssetMenu(fileName = "Targetable List", menuName = "Targeting/TargetableList")]
    public class TargetableList : ScriptableObject
    {
        private List<Targetable> _targetables = new List<Targetable>();

        public List<Targetable> Targetables { get { return _targetables; } }

        public void Add(Targetable targetable)
        {
            _targetables.Add(targetable);
        }
        public void Remove(Targetable targetable)
        {
            _targetables.Remove(targetable);
        }
    }
}