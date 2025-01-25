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

        
        #region Mono Methods

        public void Awake()
        {
            Inputs = PlayerInputs.Instance;
            
            fsmMono = new FSMMono<PlayerController>();
            fsmMono.Configure(this, moveState);
        }

        #endregion

        
        
        public void BeginAttack()
        {
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
            if (data.Hurtbox != null && data.Hurtbox.HurtResponder != null)
                data.Hurtbox.HurtResponder.Response(data);
        }
    }
}
