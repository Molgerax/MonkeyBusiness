using MonkeyBusiness.Core.FiniteStateMachine;
using MonkeyBusiness.Gameplay.Picking;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Enemies.EnemyStates
{
    public class EnemyIdleState : FSMBaseMonoState<EnemyController>
    {
        #region Serialize Fields

        [SerializeField] private float moveSpeed = 8;

        #endregion
        
        

        #region State Methods
        public override void EnterState(EnemyController entity)
        {
            //entity.AnimationController.Animator.Play("IdleWalkBlend");

        }

        public override void ExitState(EnemyController entity)
        {
        }

        public override void CheckSwitchState(EnemyController entity)
        {
            base.CheckSwitchState(entity);

            if (HotspringWater.Instance.NearestIngredient(entity.transform.position, out Ingredient ingredient,
                out float distance))
            {
                if (distance < entity.detectRadius)
                    entity.ChangeState(entity.chaseState);
            }
        }

        public override void UpdateState(EnemyController entity, float deltaTime)
        {
        }
        #endregion
    }

}
