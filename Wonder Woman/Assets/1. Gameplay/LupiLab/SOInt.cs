using UnityEngine;

namespace LupiLab
{
    [CreateAssetMenu(fileName = "Int", menuName = "LupiLab/Variables/Int")]
    public class SOInt : ScriptableObject
    {

        [SerializeField] protected int _value = 0;
        public virtual int Value { get { return _value; } }

        public virtual int SetValue(int value)
        {
            return _value = Validate(value);
        }

        public virtual int Validate(int value)
        {
            return value;
        }
    }
}