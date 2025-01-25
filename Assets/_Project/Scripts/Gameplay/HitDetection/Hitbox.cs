using MonkeyBusiness.Core.VariableReferences.GenericVariables;
using MonkeyBusiness.Utility;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.HitDetection
{
    public class Hitbox : MonoBehaviour, IHitbox
    {
        #region Serialized Fields

        [SerializeField] private Optional<Vector3> centerOffset;
        [SerializeField] private Optional<Vector3> size;
        [SerializeField] private Optional<Vector3> rotation;
        [SerializeField] private LayerMaskReference layerMask;
        [SerializeField] private ColliderType colliderShape = ColliderType.Box;
        
        #endregion

        #region Properties

        public Vector3 Center => centerOffset.Enabled ? transform.localToWorldMatrix.MultiplyPoint(centerOffset.Value)  : transform.position;
        public Vector3 Size => size.Enabled ? size.Value : transform.localScale;
        public Quaternion Rotation => rotation.Enabled ? Quaternion.Euler(rotation.Value) : transform.rotation;

        #endregion


        #region Private Fields

        private HitData _currentHitData;
        
        enum HitboxState
        {
            Open, Colliding, Closed
        }

        enum ColliderType
        {
            Box, Sphere, Capsule
        }

        private HitboxState _state = HitboxState.Closed;
        private Collider[] _cachedColliders = new Collider[16];

        private IHitResponder _hitResponder;

        #endregion


        #region Mono Methods

        private void Update()
        {
            if(_state == HitboxState.Closed) return;
            if(_hitResponder == null) return;

            CheckHit();
        }

        #endregion

        #region Interface Implementations

        public IHitResponder HitResponder
        {
            get => _hitResponder;
            set => _hitResponder = value;
        }

        public bool CheckHit()
        {
            return CheckHitbox(Center, Size, Rotation, layerMask);
        }

        public GameObject GameObject => gameObject;

        #endregion

        #region Public Methods

        public void StartCollisionCheck()
        {
            _state = HitboxState.Open;
        }

        public void StopCollisionCheck()
        {
            _state = HitboxState.Closed;
        }

        public bool IsActive() => _state == HitboxState.Open || _state == HitboxState.Colliding;
        

        public bool CheckHitbox(Vector3 center, Vector3 halfExtents, Quaternion rot, LayerMask layer)
        {
            if(_state == HitboxState.Closed) return false;

            int colliderCount = 0;
            switch (colliderShape)
            {
                case ColliderType.Box:
                    colliderCount = Physics.OverlapBoxNonAlloc(center, halfExtents, _cachedColliders, rot, layer, QueryTriggerInteraction.Collide);
                    break;
                
                case ColliderType.Sphere:
                    colliderCount = Physics.OverlapSphereNonAlloc(center, halfExtents.MaxComp(), _cachedColliders, layer, QueryTriggerInteraction.Collide);
                    break;
                    
                case ColliderType.Capsule:
                    colliderCount = Physics.OverlapCapsuleNonAlloc(center, center + (Rotation * Vector3.forward) * halfExtents.x, 
                        halfExtents.y, _cachedColliders, layer, QueryTriggerInteraction.Collide);
                    break;
            }

            bool hasValidCollision = false;

            for (int i = 0; i < Mathf.Min(colliderCount, _cachedColliders.Length) ; i++)
            {
                Collider currentCollider = _cachedColliders[i];

                if (HitboxResponse(currentCollider)) hasValidCollision = true;
            }

            _state = hasValidCollision ? HitboxState.Colliding : HitboxState.Open;

            return hasValidCollision;
        }

        private bool HitboxResponse(Collider col)
        {
            if(! col.TryGetComponent(out IHurtbox hurtbox)) return false;

            _currentHitData = new HitData()
            {
                Damage = _hitResponder?.Damage ?? 0,
                
                HitPoint = Center,
                HitNormal = Rotation * Vector3.forward,

                Hitbox = this,
                Hurtbox = hurtbox,
            };

            if (!_currentHitData.Validate()) return false;
            
            _hitResponder?.Response(_currentHitData);
            return true;
        }
        #endregion


        #region Editor

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if(_state == HitboxState.Open) Gizmos.color = Color.green;
            if(_state == HitboxState.Colliding) Gizmos.color = Color.yellow;
            
            Gizmos.matrix = Matrix4x4.TRS(Center, Rotation, Vector3.one);

            switch (colliderShape)
            {
                case ColliderType.Box:
                    Gizmos.DrawWireCube(Vector3.zero, new Vector3(Size.x * 2, Size.y * 2, Size.z * 2)); // Because size is halfExtents
                    break;
                case ColliderType.Sphere:
                    Gizmos.DrawWireSphere(Vector3.zero, Size.MaxComp()); 
                    break;
                case ColliderType.Capsule:
                    Gizmos.DrawWireSphere(Vector3.zero, Size.y);
                    Gizmos.DrawWireSphere(Vector3.forward * Size.x, Size.y);
                    Gizmos.DrawLine(Vector3.zero, Vector3.forward * Size.x);
                    break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if(_state == HitboxState.Open) Gizmos.color = Color.green;
            if(_state == HitboxState.Colliding) Gizmos.color = Color.yellow;
            
            Gizmos.matrix = Matrix4x4.TRS(Center, Rotation, Vector3.one);

            switch (colliderShape)
            {
                case ColliderType.Box:
                    Gizmos.DrawCube(Vector3.zero, new Vector3(Size.x * 2, Size.y * 2, Size.z * 2)); // Because size is halfExtents
                    break;
                case ColliderType.Sphere:
                    Gizmos.DrawSphere(Vector3.zero, Size.MaxComp()); // Because size is halfExtents
                    break;
                case ColliderType.Capsule:
                    Gizmos.DrawSphere(Vector3.zero, Size.y);
                    Gizmos.DrawSphere(Vector3.forward * Size.x, Size.y);
                    Gizmos.DrawLine(Vector3.zero, Vector3.forward * Size.x);
                    break;
            }
        }

        #endregion
    }
}
