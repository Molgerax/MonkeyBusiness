using MonkeyBusiness.Core.VariableReferences.GenericVariables;
using MonkeyBusiness.Utility;
using UltEvents;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Components
{
    public class GroundedCheck : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private Optional<Transform> detectPoint;
        [SerializeField, Range(0.0f, 2.0f)] private float detectRadius;
        [SerializeField] private LayerMaskReference detectLayers;

        [Header("Callbacks")] [SerializeField] private UltEvent<bool> OnChange;
        [SerializeField] private UltEvent OnTouchGround;
        [SerializeField] private UltEvent OnLeaveGround;

        #endregion

        #region Public Fields

        public bool Grounded;
        public Transform DetectPoint => detectPoint.Enabled ? detectPoint.Value : transform;

        #endregion

        #region Mono Methods

        private void Update()
        {
            IsGrounded();
        }

        #endregion


        #region Public Methods



        #endregion

        public bool IsGrounded()
        {
            Vector3 spherePosition = DetectPoint.position;

            bool currentCheck = Physics.CheckSphere(spherePosition, detectRadius, detectLayers,
                QueryTriggerInteraction.Ignore);

            if (currentCheck != Grounded)
                OnChange?.Invoke(currentCheck);

            if (currentCheck)
            {
                if (!Grounded) OnTouchGround?.Invoke();
                Grounded = true;
            }
            else
            {
                if (Grounded) OnLeaveGround?.Invoke();
                Grounded = false;
            }

            return Grounded;
        }


        #region Editor

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Grounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(DetectPoint.position, detectRadius);
        }

        #endregion
    }
}