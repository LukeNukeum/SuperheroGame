using UnityEngine;

namespace LupiLab
{
    [CreateAssetMenu(fileName = "Float", menuName = "LupiLab/Variables/Float")]
    public class SOFloat : ScriptableObject
    {

        [SerializeField] protected float _value = 0;
        public virtual float Value { get { return _value; } }

        public virtual float SetValue(float value)
        {
            return _value = Validate(value);
        }

        public virtual float Validate(float value)
        {
            return value;
        }
    }
}