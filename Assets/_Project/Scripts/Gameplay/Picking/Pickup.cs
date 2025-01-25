using System;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Picking
{
    public class Pickup : MonoBehaviour
    {
        public bool IsHeld = false;
        public PickupHolder Holder = null;
        
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }


        public void BeginHold(PickupHolder holder)
        {
            Holder = holder;
            IsHeld = true;

            transform.parent = holder.transform;
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.isKinematic = true;
        }

        public void EndHold()
        {
            Holder = null;
            IsHeld = false;

            transform.parent = null;

            _rb.isKinematic = false;
        }
    }
}
