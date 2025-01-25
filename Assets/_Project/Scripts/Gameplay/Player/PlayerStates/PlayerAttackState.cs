using MonkeyBusiness.Core.FiniteStateMachine;
using MonkeyBusiness.Input;
using MonkeyBusiness.Utility;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Player.PlayerStates
{
    public class PlayerAttackState : FSMBaseMonoState<PlayerController>
    {
        #region Serialize Fields

        [Header("Durations")]
        [SerializeField] private float windUpTime = 0.2f;
        [SerializeField] private float attackDuration = 0.5f;
        [SerializeField] private float endingTime = 0.2f;

        [Header("Params")] 
        [SerializeField, Range(0, 30)] private float rotationSpeed = 15;
        
        #endregion

        private float _attackTime = 0;
        private bool _isHitboxActive;
        
        #region State Methods
        public override void EnterState(PlayerController entity)
        {
            //entity.AnimationController.Animator.Play("Attack");

            _attackTime = 0f;
        }

        public override void ExitState(PlayerController entity)
        {
            entity.EndAttack();
        }

        public override void CheckSwitchState(PlayerController entity)
        {
            base.CheckSwitchState(entity);

            if (_attackTime >= windUpTime + attackDuration && entity.Inputs.ExtendPressed)
            {
                entity.ChangeState(entity.extendState);
                return;
            }
            
            if (_attackTime >= windUpTime + attackDuration + endingTime)
                entity.ChangeState(entity.moveState);
        }

        public override void UpdateState(PlayerController entity, float deltaTime)
        {
            //entity.AnimationController.Animator.SetFloat("Speed", entity.MoveController.Motor.BaseVelocity.magnitude);

            _attackTime += deltaTime;

            if (_attackTime < windUpTime)
            {
                entity.Movement.RotatePlayerTowardsCamera();
            }
            
            if (_attackTime > windUpTime && !entity.Hitbox.IsActive())
            {
                entity.BeginAttack();
                entity.PickupHolder.DropPickup();
            }

            if (_attackTime > windUpTime + attackDuration && entity.Hitbox.IsActive())
            {
                entity.EndAttack();
            }
        }

        private Vector3 FlattenEulerAngles(Vector3 eulerAngles)
        {
            Quaternion rotation = Quaternion.Euler(eulerAngles);
            Vector3 flatForward = (rotation * Vector3.forward).FlattenY().normalized;

            return Quaternion.LookRotation(flatForward, Vector3.up).eulerAngles;
        }
        
        #endregion
        
    }

}