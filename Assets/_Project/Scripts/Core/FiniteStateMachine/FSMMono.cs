namespace BFB.Core.FSM
{
    public class FSMMono<T> where T : StatefulMonoBehaviour<T>
    {
        private T Owner;
        private FSMBaseMonoState<T> CurrentState;

        public void Configure(T owner, FSMBaseMonoState<T> initialState)
        {
            Owner = owner;
            ChangeState(initialState);
        }

        public void Update(float deltaTime)
        {
            if (CurrentState != null)
            {
                CurrentState.CheckSwitchState(Owner);
                CurrentState.UpdateStates(Owner, deltaTime);
            }
        }

        public void ChangeState(FSMBaseMonoState<T> newState)
        {
            if (CurrentState != null)
            {
                CurrentState.ExitStates(Owner);
                CurrentState.OnExit?.Invoke();
                CurrentState.gameObject.SetActive(false);


                if (CurrentState.IsRootState)
                    CurrentState = newState;
                else if (CurrentState.CurrentSuperState != null)
                    CurrentState.CurrentSuperState.SetSubState(newState);
            }
            else
            {
                CurrentState = newState;
            }

            if (CurrentState != null)
            {
                CurrentState.gameObject.SetActive(true);

                CurrentState.InitializeSubState(Owner);

                CurrentState.EnterState(Owner);
                CurrentState.OnEnter?.Invoke();
            }
        }
        
        public void ChangeState(FSMBaseMonoState<T> newState, bool ignoreSameState)
        {
            if(CurrentState == newState && ignoreSameState) return;
            
            if (CurrentState != null)
            {
                CurrentState.ExitStates(Owner);
                CurrentState.OnExit?.Invoke();
                CurrentState.gameObject.SetActive(false);


                if (CurrentState.IsRootState)
                    CurrentState = newState;
                else if (CurrentState.CurrentSuperState != null)
                    CurrentState.CurrentSuperState.SetSubState(newState);
            }
            else
            {
                CurrentState = newState;
            }

            if (CurrentState != null)
            {
                CurrentState.gameObject.SetActive(true);

                CurrentState.InitializeSubState(Owner);

                CurrentState.EnterState(Owner);
                CurrentState.OnEnter?.Invoke();
            }
        }
    }
}