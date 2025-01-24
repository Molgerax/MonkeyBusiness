using System;
using UnityEngine;

namespace MonkeyBusiness.Core.EventSystem.Generic_Events.EventListeners
{
    public abstract class AbstractListener : MonoBehaviour
    {
        [Serializable]
        public enum ResponseMode {
            InvokeUnityEvents,
            InvokeCSharpEvents
        }

        [SerializeField] protected ResponseMode _responseActivationMode;

        public ResponseMode ResponseActivationMode { get => _responseActivationMode; set => _responseActivationMode = value; }
    }
}