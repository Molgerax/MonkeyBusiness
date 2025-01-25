using MonkeyBusiness.Core.FiniteStateMachine;
using MonkeyBusiness.Gameplay.HitDetection;
using MonkeyBusiness.Gameplay.Picking;
using MonkeyBusiness.Gameplay.Player;
using MonkeyBusiness.Input;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Enemies
{
    public class EnemyController : StatefulMonoBehaviour<EnemyController>, IHurtResponder
    {
        
        #region Public Fields

        [SerializeField] public float detectRadius = 10;
        [SerializeField] public GenericHurtbox hurtbox;
        
        
        public Ingredient currentTarget = null;
        
        #endregion

        private Rigidbody _rb;
        
        #region State Machine

        [SerializeField] public FSMBaseMonoState<EnemyController> idleState;
        [SerializeField] public FSMBaseMonoState<EnemyController> stunnedState;
        [SerializeField] public FSMBaseMonoState<EnemyController> chaseState;

        #endregion

        
        #region Mono Methods

        public void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            fsmMono = new FSMMono<EnemyController>();
            fsmMono.Configure(this, idleState);

            hurtbox.HurtResponder = this;
        }

        #endregion


        public void MoveEnemyTowards(Vector3 target, float speed, float deltaTime)
        {
            Vector3 newPos = Vector3.MoveTowards(_rb.position, target, speed * deltaTime);

            _rb.MovePosition(newPos);
        }
        

        #region Hit Detection
        
        public void Response(HitData data)
        {
            fsmMono.ChangeState(stunnedState);
        }

        public bool CheckHit(HitData data)
        {
            return data.Damage > 0;
        }
        
        #endregion
    }
}
