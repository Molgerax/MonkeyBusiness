using MonkeyBusiness.Utility;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.HitDetection
{
    public class GenericHurtbox : MonoBehaviour, IHurtbox
    {
        #region Serialize Fields

        [SerializeField] private Optional<bool> m_active;
        [SerializeField] private Optional<Transform> m_transform;
        [SerializeField] private Optional<GameObject> m_gameObject;
        
        #endregion

        private bool _active = true;
        
        #region Public Methods

        public void SetHitboxActive(bool value)
        {
            _active = value;
        }

        #endregion
        
        #region Interface Implementations

        public bool Active => m_active.Enabled ? m_active : _active;
        public GameObject GameObject => m_gameObject.Enabled ? m_gameObject : gameObject;
        public Transform Transform => m_transform.Enabled ? m_transform : transform;
        public IHurtResponder HurtResponder { get; set; }

        public bool CheckHit(HitData hitData)
        {
            return Active;
        }
        #endregion
    }
}
