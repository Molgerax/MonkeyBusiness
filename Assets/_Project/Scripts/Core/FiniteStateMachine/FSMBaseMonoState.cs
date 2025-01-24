using System;
using BFB.Core.FSM.StateTransitions;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;

namespace BFB.Core.FSM
{
    [DefaultExecutionOrder(-10)]
    public abstract class FSMBaseMonoState<T> : MonoBehaviour, IFSMState<T> where T : StatefulMonoBehaviour<T>
    {
        #region Unity Events

        public UltEvent OnEnter;
        public UltEvent OnExit;

        #endregion

        #region State Transitions

        [SerializeField] protected BaseStateTransition<T>[] stateTransitions;

        protected void CheckStateTransitions(T entity)
        {
            if (stateTransitions == null) return;
            for (int i = 0; i < stateTransitions.Length; i++)
            {
                if( stateTransitions[i].CheckSwitchState(entity))
                    break;
            }
        }
        
        #endregion

        public void Awake()
        {
            gameObject.SetActive(false);
            stateTransitions = GetComponents<BaseStateTransition<T>>();
        }

        public abstract void EnterState(T entity);
        public abstract void ExitState(T entity);

        public virtual void CheckSwitchState(T entity)
        {
            CheckStateTransitions(entity);
        }
        public abstract void UpdateState(T entity, float deltaTime);

        #region Hierarchical State Machine
        
        protected FSMBaseMonoState<T> _currentSubState;
        protected FSMBaseMonoState<T> _currentSuperState;

        public FSMBaseMonoState<T> CurrentSuperState => _currentSuperState;
        public FSMBaseMonoState<T> CurrentSubState => _currentSubState;

        
        public bool IsRootState => _currentSuperState == null;
        
        public virtual void InitializeSubState(T entity) {}

        public void UpdateStates(T entity, float deltaTime)
        {
            UpdateState(entity, deltaTime);
            if (_currentSubState != null)
            {
                _currentSubState.UpdateStates(entity, deltaTime);
            }
        }

        public void EnterStates(T entity)
        {
            EnterState(entity);
            if (_currentSubState != null)
            {
                _currentSubState.EnterStates(entity);
            }
        }
        
        public void ExitStates(T entity)
        {
            if (_currentSubState != null)
            {
                _currentSubState.ExitStates(entity);
                _currentSubState.gameObject.SetActive(false);
            }
            ExitState(entity);
        }
        
        public void SetSuperState(FSMBaseMonoState<T> newSuperState)
        {
            _currentSuperState = newSuperState;
        }
        
        public void SetSubState(FSMBaseMonoState<T> newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }

        #endregion
    }
}