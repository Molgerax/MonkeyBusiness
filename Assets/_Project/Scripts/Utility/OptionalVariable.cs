using System;
using UnityEngine;

// Cool thing from this video, for ease of use and visibility:
// https://www.youtube.com/watch?v=uZmWgQ7cLNI


namespace MonkeyBusiness.Utility
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField] private bool enabled;
        [SerializeField] private T value;

        public Optional(T initialValue, bool enabled = false)
        {
            this.enabled = enabled;
            value = initialValue;
        }

        public bool Enabled => enabled;
        public T Value => value;

        public static implicit operator T(Optional<T> optional)
        {
            return optional.enabled ? optional.Value : default;
        }
    }
}
