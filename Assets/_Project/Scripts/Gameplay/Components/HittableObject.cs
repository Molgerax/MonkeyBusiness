using MonkeyBusiness.Gameplay.HitDetection;
using UltEvents;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Components
{
    public class HittableObject : MonoBehaviour, IHurtResponder
    {
        #region Serialize Fields

        [SerializeField] private GenericHurtbox hurtbox;
        [SerializeField] private UltEvent onHit;

        [SerializeField] private bool isHittable = true;

        #endregion

       
        #region Public Methods

        public bool IsHittable { get; set; }
        
        #endregion

        #region Mono Methods

        private void Awake()
        {
            hurtbox.HurtResponder = this;
            IsHittable = isHittable;
        }

        #endregion


        #region Interfaces

        public void Response(HitData data)
        {
            onHit?.Invoke();
        }

        public bool CheckHit(HitData data)
        {
            return IsHittable;
        }
        #endregion
    }

}
