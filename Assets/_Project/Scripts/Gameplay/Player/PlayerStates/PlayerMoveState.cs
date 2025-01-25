using MonkeyBusiness.Core.FiniteStateMachine;
using MonkeyBusiness.Input;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Player.PlayerStates
{
    public class PlayerMoveState : FSMBaseMonoState<PlayerController>
    {
        #region Serialize Fields

        [SerializeField] private float moveSpeed = 300;

        #endregion
        
        

        #region State Methods
        public override void EnterState(PlayerController entity)
        {
            //entity.AnimationController.Animator.Play("IdleWalkBlend");

            entity.SuperCharacterAio.walkingSpeed = moveSpeed;
        }

        public override void ExitState(PlayerController entity)
        {
            entity.SuperCharacterAio.walkingSpeed = 0;
        }

        public override void CheckSwitchState(PlayerController entity)
        {
            base.CheckSwitchState(entity);
        }

        public override void UpdateState(PlayerController entity, float deltaTime)
        {
            //entity.AnimationController.Animator.SetFloat("Speed", entity.MoveController.Motor.BaseVelocity.magnitude);
            
            if (entity.Inputs.AttackPressed)
                entity.ChangeState(entity.attackState);
            
            if (entity.Inputs.ExtendPressed)
                entity.ChangeState(entity.extendState);
        }
        #endregion
    }

}
