using MonkeyBusiness.Core.FiniteStateMachine;
using MonkeyBusiness.Input;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Player.PlayerStates
{
    public class PlayerMoveState : FSMBaseMonoState<PlayerController>
    {
        #region Serialize Fields

        [SerializeField] private float moveSpeed = 8;

        #endregion
        
        

        #region State Methods
        public override void EnterState(PlayerController entity)
        {
            //entity.AnimationController.Animator.Play("IdleWalkBlend");

        }

        public override void ExitState(PlayerController entity)
        {
        }

        public override void CheckSwitchState(PlayerController entity)
        {
            base.CheckSwitchState(entity);
            
            if (entity.Inputs.AttackPressed)
                entity.ChangeState(entity.attackState);
            
            if (entity.Inputs.ExtendPressed)
                entity.ChangeState(entity.extendState);
        }

        public override void UpdateState(PlayerController entity, float deltaTime)
        {
            //entity.AnimationController.Animator.SetFloat("Speed", entity.MoveController.Motor.BaseVelocity.magnitude);
            
            entity.Movement.MovePlayer(moveSpeed, deltaTime);
        }
        #endregion
    }

}
