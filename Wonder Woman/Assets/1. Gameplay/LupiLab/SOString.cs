using UnityEngine;

namespace LupiLab
{
    [CreateAssetMenu(fileName = "String", menuName = "LupiLab/Variables/String")]
    public class SOString : ScriptableObject
    {

        [SerializeField] protected string _value = "";
        public virtual string Value { get { return _value; } }

        public virtual string SetValue(string value)
        {
            return _value = Validate(value);
        }

        public virtual string Validate(string value)
        {
            return value;
        }
    }
}