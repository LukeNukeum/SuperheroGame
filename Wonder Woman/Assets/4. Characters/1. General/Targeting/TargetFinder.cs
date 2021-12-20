using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Targeting
{
    public abstract class TargetFinder : ScriptableObject
    {
        public abstract Targetable GetTarget(ISearcher hunter);
        public abstract bool Evaluate(ISearcher hunter, Targetable targetable);
    }
}