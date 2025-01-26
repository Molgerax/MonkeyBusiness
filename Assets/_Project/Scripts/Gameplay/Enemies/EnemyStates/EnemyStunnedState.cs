using MonkeyBusiness.Core.FiniteStateMachine;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Enemies.EnemyStates
{
    public class EnemyStunnedState : FSMBaseMonoState<EnemyController>
    {
        #region Serialize Fields

        [SerializeField] private float stunDuration = 3;

        #endregion

        private float _stunTime = 0;

        #region State Methods
        public override void EnterState(EnemyController entity)
        {
            //entity.AnimationController.Animator.Play("IdleWalkBlend");
            _stunTime = 0;
            FMODUnity.RuntimeManager.PlayOneShot("A_SFX_Hit");
            FMODUnity.RuntimeManager.PlayOneShot("A_MNKY_MonkeyHit");
        }

        public override void ExitState(EnemyController entity)
        {
        }

        public override void CheckSwitchState(EnemyController entity)
        {
            base.CheckSwitchState(entity);
            
            
            if (_stunTime >= stunDuration)
                entity.ChangeState(entity.idleState);
        }

        public override void UpdateState(EnemyController entity, float deltaTime)
        {
            _stunTime += deltaTime;
        }
        #endregion
    }

}
