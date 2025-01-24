using MonkeyBusiness.Core.VariableReferences.GenericVariables;
using UltEvents;
using UnityEngine;

namespace MonkeyBusiness.Core.EventSystem
{
    public class IntCompareComponent : MonoBehaviour
    {
        #region Serialize Fields
        
        [SerializeField] private IntReference compareValue;
        [SerializeField] private UltEvent onTrue;

        #endregion

        #region Public Methods

        public void Equals(int value)
        {
            if(value == compareValue) onTrue?.Invoke();
        }
        
        public void Greater(int value)
        {
            if(value > compareValue) onTrue?.Invoke();
        }
        
        public void Less(int value)
        {
            if(value < compareValue) onTrue?.Invoke();
        }
        
        
        public void LessEquals(int value)
        {
            if(value <= compareValue) onTrue?.Invoke();
        }
        
        
        public void GreaterEquals(int value)
        {
            if(value >= compareValue) onTrue?.Invoke();
        }
        
        
        public void NotEquals(int value)
        {
            if(value != compareValue) onTrue?.Invoke();
        }

        #endregion
    }
}
