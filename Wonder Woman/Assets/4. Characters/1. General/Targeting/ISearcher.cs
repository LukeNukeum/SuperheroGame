using System.Collections.Generic;
using UnityEngine;

namespace LupiLab.Targeting
{
    public interface ISearcher
    {
        Vector3 Position { get; }
        Vector3 Direction { get; }
        IEnumerable<Collider> Triggers { get; }
        Targetable ThisTargetable { get; }
        Targetable Target { get; }
        bool HasTarget { get; }

        bool GetTarget();
        bool ValidateCurrentTarget();


    }
}