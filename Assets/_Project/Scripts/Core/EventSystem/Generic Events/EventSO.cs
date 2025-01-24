using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFB.Core.Events
{
    public abstract class BaseEventSO : ScriptableObject
    {
        
    }
    
    public abstract class EventSO<T> : BaseEventSO
    {
        public event UnityAction<T> action;

        protected HashSet<BaseEventListener<T>> _listeners = new HashSet<BaseEventListener<T>>();

        public IReadOnlyCollection<BaseEventListener<T>> Listeners => _listeners;

        public bool RegisterListener(BaseEventListener<T> listener) {
            return _listeners.Add(listener);
        }

        public void UnregisterListener(BaseEventListener<T> listener) {
            _listeners.Remove(listener);
        }
        
        public void Raise(T data) 
        {
            foreach (BaseEventListener<T> listener in _listeners) {
                listener.OnEventRaised(data);
            }
            action?.Invoke(data);
        }
        
    }
}