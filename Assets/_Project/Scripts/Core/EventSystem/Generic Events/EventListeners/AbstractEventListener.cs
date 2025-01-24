using System;
using UnityEngine;

namespace BFB.Core.Events
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