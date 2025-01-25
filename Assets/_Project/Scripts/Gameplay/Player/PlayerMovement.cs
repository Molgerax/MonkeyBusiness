using System;
using MonkeyBusiness.Utility;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerController controller;
        [SerializeField] private Transform inputReference;
        [SerializeField] private Transform cameraHead;
        [SerializeField] private float moveSpeed = 5;

        

        private Rigidbody _rb;

        private float _cameraY;
        private float _cameraX;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            RotateCamera();
        }


        private Vector3 GetRelativeInputDirection()
        {
            Transform t = inputReference ? inputReference : transform;

            Vector3 relativeInput = t.forward * controller.Inputs.MoveInput.y
                                    + t.right * controller.Inputs.MoveInput.x;
            relativeInput.y = 0;
            return relativeInput.normalized * Mathf.Min(1, controller.Inputs.MoveInput.magnitude);
        }

        public void MovePlayer(float speed, float deltaTime)
        {
            Vector3 input = GetRelativeInputDirection();

            _rb.position += input * speed * deltaTime;
            
            RotatePlayerTowardsWalk();
        }

        private void RotatePlayerTowardsWalk()
        {
            Vector3 input = GetRelativeInputDirection();
            if (input.magnitude == 0)
                return;
            
            Quaternion targetRotation = Quaternion.LookRotation(input, Vector3.up);
            Quaternion currentRotation = transform.rotation;
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 0.5f);
        }

        public void RotatePlayerTowardsCamera()
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraHead.forward.FlattenY().normalized, Vector3.up);
            Quaternion currentRotation = transform.rotation;
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 0.5f);
        }
        
        public void RotateCamera()
        {
            cameraHead.position = transform.position + Vector3.up * 1.8f;

            Vector2 lookInput = controller.Inputs.LookInput;

            _cameraX = Mathf.Clamp(_cameraX + lookInput.y, -10, 45);
            _cameraX = 35;
            _cameraY += lookInput.x;

            _cameraY %= 720;
            
            cameraHead.eulerAngles = new Vector3(_cameraX, _cameraY, 0);
        }
    }
}
