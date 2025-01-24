using UnityEngine;

namespace MonkeyBusiness.Core.EventSystem.Generic_Events
{
    [CreateAssetMenu(fileName = "Void Event", menuName = "BFB/Events/Void Event")]
    public class VoidEventSO : EventSO<Void>
    {
        public void Raise() => Raise(new Void());
    }

    public struct Void
    {
        
    }
}