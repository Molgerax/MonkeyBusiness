using System;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerController controller;
        [SerializeField] private Transform inputReference;
        [SerializeField] private float moveSpeed = 5;


        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            
        }


        private Vector3 GetRelativeInputDirection()
        {
            Transform t = inputReference ? inputReference : transform;

            Vector3 relativeInput = t.forward * controller.Inputs.MoveInput.y
                                    + t.right * controller.Inputs.MoveInput.x;
            relativeInput.y = 0;
            return relativeInput.normalized * Mathf.Min(1, controller.Inputs.MoveInput.magnitude);
        }
    }
}
