using MonkeyBusiness.Core.FiniteStateMachine;
using MonkeyBusiness.Gameplay.Picking;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Enemies.EnemyStates
{
    public class EnemyChaseState : FSMBaseMonoState<EnemyController>
    {
        #region Serialize Fields

        [SerializeField] private float moveSpeed = 8;

        #endregion

        private Ingredient _currentTarget;

        #region State Methods
        public override void EnterState(EnemyController entity)
        {
            //entity.AnimationController.Animator.Play("IdleWalkBlend");

            if (HotspringWater.Instance.NearestIngredient(entity.transform.position, out Ingredient ingredient,
                out float distance))
            {
                _currentTarget = ingredient;
            }
        }

        public override void ExitState(EnemyController entity)
        {
        }

        public override void CheckSwitchState(EnemyController entity)
        {
            base.CheckSwitchState(entity);

            if (_currentTarget == null)
            {
                entity.ChangeState(entity.idleState);
                return;
            }

            if (Vector3.Distance(entity.transform.position, _currentTarget.transform.position) >= entity.detectRadius)
            {
                entity.ChangeState(entity.idleState);
                return;
            }
        }

        public override void UpdateState(EnemyController entity, float deltaTime)
        {
            HotspringWater.Instance.NearestIngredient(entity.transform.position, out _currentTarget,
                out float distance);

            if (_currentTarget == null)
                return;

            Vector3 targetPos = _currentTarget.transform.position;
            targetPos.y = HotspringWater.Instance.WaterLevel;
            
            entity.MoveEnemyTowards(targetPos, moveSpeed, deltaTime);
        }
        #endregion
    }

}
