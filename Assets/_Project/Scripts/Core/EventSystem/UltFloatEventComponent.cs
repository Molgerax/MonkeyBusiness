using UltEvents;
using UnityEngine;

namespace BFB.Core.Events
{
    public class UltFloatEventComponent : MonoBehaviour
    {
        #region Serialize Fields
        [SerializeField] private UltEvent<float> unityEvent;

        [Header("Manipulators")]
        [SerializeField] private float multiplier = 1f;
        [SerializeField] private float addend = 0f;
        
        #endregion
        
        public void Invoke(float value)
        {

            unityEvent?.Invoke(value * multiplier + addend);
        }
        
        public void Invoke(int value)
        {

            unityEvent?.Invoke(value * multiplier + addend);
        }
    }
}
