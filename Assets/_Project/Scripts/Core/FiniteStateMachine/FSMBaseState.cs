using System;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;

namespace BFB.Core.FSM
{
    public interface IFSMState<T>
    {
        public void EnterState(T entity);
        public void ExitState(T entity);
        public void CheckSwitchState(T entity);
        public void UpdateState(T entity, float deltaTime);
        

        public void UpdateStates(T entity, float deltaTime);

        public void EnterStates(T entity);
        public void ExitStates(T entity);
    }
    
    public abstract class FSMBaseState<T> : IFSMState<T>
    {
        public abstract void EnterState(T entity);
        public abstract void ExitState(T entity);
        public abstract void CheckSwitchState(T entity);
        public abstract void UpdateState(T entity, float deltaTime);

        #region Hierarchical State Machine
        
        protected FSMBaseState<T> _currentSubState;
        protected FSMBaseState<T> _currentSuperState;

        public FSMBaseState<T> CurrentSuperState => _currentSuperState;
        public FSMBaseState<T> CurrentSubState => _currentSubState;

        
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
            ExitState(entity);
            if (_currentSubState != null)
            {
                _currentSubState.ExitStates(entity);
            }
        }
        
        public void SetSuperState(FSMBaseState<T> newSuperState)
        {
            _currentSuperState = newSuperState;
        }
        
        public void SetSubState(FSMBaseState<T> newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }

        #endregion
    }
}