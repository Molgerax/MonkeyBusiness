using System;
using UltEvents;
using UnityEngine;

namespace BFB.Core.Events
{
    
    
    public class BaseEventListener<T> : AbstractListener
    {
        [SerializeField] EventSO<T> _event;

        public EventSO<T> Event { get => _event;
            set {
                if(_event) _event.UnregisterListener(this);
                _event = value;
                _event.RegisterListener(this);
            }
        }

        // C# Event
        public event Action<T> Response;

        // Unity Event
        [SerializeField] public UltEvent<T> _unityEventResponse;


        private void OnEnable() 
        {
            if(Event) Event.RegisterListener(this);
        }

        private void OnDisable() 
        {
            if(Event) Event.UnregisterListener(this);
        }

        public void OnEventRaised(T data) {
            switch (_responseActivationMode) {
                case ResponseMode.InvokeUnityEvents:
                    _unityEventResponse.Invoke(data);
                    break;
                case ResponseMode.InvokeCSharpEvents:
                    Response?.Invoke(data);
                    break;
            }
        }
    }
}
