using MonkeyBusiness.Core.FiniteStateMachine;
using MonkeyBusiness.Gameplay.Animations;
using MonkeyBusiness.Gameplay.HitDetection;
using MonkeyBusiness.Gameplay.Picking;
using MonkeyBusiness.Input;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Player
{
    public class PlayerController : StatefulMonoBehaviour<PlayerController>, IHitResponder
    {
        #region Serialized Fields

        [SerializeField] private Hitbox hitbox;
        public Hitbox Hitbox => hitbox;

        public PickupHolder PickupHolder;
        
        #endregion

        #region Public Fields

        public PlayerMovement Movement;
        public AnimationController AnimationController;
        public PlayerInputs Inputs;

        #endregion
        
        #region State Machine

        [SerializeField] public FSMBaseMonoState<PlayerController> moveState;
        [SerializeField] public FSMBaseMonoState<PlayerController> attackState;
        [SerializeField] public FSMBaseMonoState<PlayerController> extendState;

        #endregion

        private IHurtResponder[] _hitObjects = new IHurtResponder[16];
        private int _hitObjectCount = 0;
        
        #region Mono Methods

        public void Awake()
        {
            Inputs = PlayerInputs.Instance;
            
            fsmMono = new FSMMono<PlayerController>();
            fsmMono.Configure(this, moveState);
        }

        #endregion

        private void ClearArray()
        {
            for (int i = 0; i < _hitObjects.Length; i++)
            {
                _hitObjects[i] = null;
            }

            _hitObjectCount = 0;
        }

        private bool IsKickedInArray(IHurtResponder kickable)
        {
            for (int i = 0; i < _hitObjectCount; i++)
            {
                if (_hitObjects[i] == kickable) return true;
            }

            return false;
        }

        
        public void BeginAttack()
        {
            ClearArray();
            hitbox.HitResponder = this;
            hitbox.StartCollisionCheck();
        }

        public void EndAttack()
        {
            hitbox.HitResponder = null;
            hitbox.StopCollisionCheck();
        }

        public int Damage => 1;
        public bool CheckHit(HitData data)
        {
            return true;
        }

        public void Response(HitData data)
        { 
            if (data.Hurtbox == null)
               return;

            if (data.Hurtbox.HurtResponder == null)
                return;

            IHurtResponder responder = data.Hurtbox.HurtResponder;
            if (IsKickedInArray(responder))
                return;

            _hitObjects[_hitObjectCount++] = responder;
            data.Hurtbox.HurtResponder.Response(data);
        }
    }
}
