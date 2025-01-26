using MonkeyBusiness.Core.FiniteStateMachine;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Enemies.EnemyStates
{
    public class EnemyEatState : FSMBaseMonoState<EnemyController>
    {
        #region Serialize Fields

        [SerializeField] private float eatDuration = 2;

        #endregion

        private float _eatTime = 0;

        #region State Methods
        public override void EnterState(EnemyController entity)
        {
            //entity.AnimationController.Animator.Play("IdleWalkBlend");
            _eatTime = 0;
        }

        public override void ExitState(EnemyController entity)
        {
        }

        public override void CheckSwitchState(EnemyController entity)
        {
            base.CheckSwitchState(entity);
            
            
            if (_eatTime >= eatDuration)
                entity.ChangeState(entity.idleState);
        }

        public override void UpdateState(EnemyController entity, float deltaTime)
        {
            _eatTime += deltaTime;
            
        }
        #endregion
    }

}
