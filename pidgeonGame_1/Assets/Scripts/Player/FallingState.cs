using UnityEngine;

public class FallingState : BasePlayerState
{
    public FallingState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.meleeAttackState);

        if (player.IsGrounded())
        {
            if (Mathf.Abs(player.horizontal) > 0.01f)
                stateMachine.ChangeState(player.runningState);
            else
                stateMachine.ChangeState(player.idleState);
        }

        if (Input.GetKey(KeyCode.E) && player.glideTimeLeft > 0f)
            stateMachine.ChangeState(player.glidingState);
    }
}
