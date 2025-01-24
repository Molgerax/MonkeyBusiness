using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyBusiness.Input
{
        [DefaultExecutionOrder(-100)]
    public class PlayerInputs : MonoBehaviour
    {
        public static PlayerInputs Instance;
        
        #region Serialize Fields

        [SerializeField] [Range(0.01f, 5f)] private float mouseSensitivity = 1;
        
        #endregion

        #region Public Fields

        public InputAction moveAction;
        public InputAction lookAction;
        public InputAction attackAction;
        public InputAction extendAction;
        
        #endregion

        #region Private Fields
        private PlayerInputActions _inputs;

        #endregion

        #region Properties

        public Vector2 MoveInput => moveAction.ReadValue<Vector2>();
        public Vector2 LookInput => lookAction.ReadValue<Vector2>() * mouseSensitivity;

        public bool AttackPressed => attackAction.IsPressed();
        public bool ExtendPressed => extendAction.IsPressed();
        

        #endregion


        #region Mono Methods

        private void Awake()
        {
            _inputs = new PlayerInputActions();
            _inputs.Enable();

            Instance = this;
            
            moveAction = _inputs.Player.Move;
            lookAction = _inputs.Player.Look;
            attackAction = _inputs.Player.Attack;
            extendAction = _inputs.Player.Extend;
        }

        private void OnEnable()
        {
        }

        #endregion




        #region Input Callbacks

        
        #endregion
    }
}