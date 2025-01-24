namespace BFB.Core.FSM
{
    public class FSM<T, TState> where TState : FSMBaseState<T>
    {
        private T Owner;
        private TState CurrentState;

        public TState State => CurrentState;

        public void Configure(T owner, TState initialState)
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
        
        public void ChangeState(TState newState) 
        {
            if (CurrentState != null)
            {
                CurrentState.ExitStates(Owner);
                
                
                if(CurrentState.IsRootState)
                    CurrentState = newState;
                else if(CurrentState.CurrentSuperState != null)
                    CurrentState.CurrentSuperState.SetSubState(newState);
            }
            else
            {
                CurrentState = newState;
            }

            if (CurrentState != null)
            {
                CurrentState.InitializeSubState(Owner);
                
                CurrentState.EnterState(Owner);
            }
        }
    }
}