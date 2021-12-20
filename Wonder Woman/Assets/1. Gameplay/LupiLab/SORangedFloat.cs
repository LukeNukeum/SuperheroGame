using UnityEngine;

namespace LupiLab
{
    [ExecuteAlways]
    [CreateAssetMenu(fileName = "Ranged Float", menuName = "LupiLab/Variables/Ranged Float")]
    public class SORangedFloat : SOFloat
    {
        [SerializeField] protected float _max = 100;
        [SerializeField] protected float _min = 0; //Todo: make ranged float validate entered values

        public float Max { get { return _max; } }
        public float Min { get { return _min; } }

        //public override float SetValue(float value)
        //{
        //    return _value = Validate(value);
        //}

        public override float Validate(float value)
        {
            return Mathf.Clamp(value, _min, _max);
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            _value = Mathf.Clamp(_value, _min, _max);
        }
    }
#endif
}