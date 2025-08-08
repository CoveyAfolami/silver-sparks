public class PlayerStateMachine
{
    public BasePlayerState CurrentState { get; private set; }

    public void Initialize(BasePlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(BasePlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
