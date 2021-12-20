using UnityEngine;


namespace LupiLab.Camera
{
    public abstract class CameraRig : MonoBehaviour
    {
        public abstract Quaternion ForwardFlatAngle { get; }
        public abstract Quaternion TrueForwardAngle { get; }
        public abstract UnityEngine.Camera Camera { get; }

        public abstract void SetTarget(Transform target);
    }
}