using MonkeyBusiness.Core.FiniteStateMachine;
using MonkeyBusiness.Input;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Player.PlayerStates
{
    public class PlayerExtendState : FSMBaseMonoState<PlayerController>
    {
        #region Serialize Fields

        [Header("Durations")]
        [SerializeField] private float windUpTime = 0.2f;
        [SerializeField] private float attackDuration = 0.5f;
        [SerializeField] private float endingTime = 0.2f;

        [Header("Params")] 
        [SerializeField, Range(0, 30)] private float rotationSpeed = 15;
        
        #endregion

        private float _extendTime = 0;
        private bool _isHitboxActive;
        
        #region State Methods
        public override void EnterState(PlayerController entity)
        {
            //entity.AnimationController.Animator.Play("Attack");

            _extendTime = 0f;
            
            entity.PickupHolder.BeginPicking();
        }

        public override void ExitState(PlayerController entity)
        {
            entity.PickupHolder.EndPicking();
        }

        public override void CheckSwitchState(PlayerController entity)
        {
            base.CheckSwitchState(entity);

            if (entity.Inputs.ExtendPressed == false)
            {
                entity.ChangeState(entity.moveState);
                return;
            }
            
            if (entity.Inputs.AttackPressed)
                entity.ChangeState(entity.attackState);
        }

        public override void UpdateState(PlayerController entity, float deltaTime)
        {
            //entity.AnimationController.Animator.SetFloat("Speed", entity.MoveController.Motor.BaseVelocity.magnitude);

            _extendTime += deltaTime;
            
            entity.Movement.RotatePlayerTowardsCamera();
        }
        
        #endregion
        
    }

}