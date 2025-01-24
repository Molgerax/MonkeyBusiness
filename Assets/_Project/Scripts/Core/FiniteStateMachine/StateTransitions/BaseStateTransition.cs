using BFB.Core.FSM;
using UnityEngine;

namespace BFB.Core.FSM.StateTransitions
{
    public abstract class BaseStateTransition<T> : MonoBehaviour where T : StatefulMonoBehaviour<T>
    {
        #region Serialize Fields

        [SerializeField] public FSMBaseMonoState<T> targetState;

        #endregion


        public virtual bool CheckSwitchState(T entity)
        {
            if (IsTransitionValid(entity))
            {
                entity.ChangeState(targetState);
                return true;
            }

            return false;
        }

        protected abstract bool IsTransitionValid(T entity);
    }
}
