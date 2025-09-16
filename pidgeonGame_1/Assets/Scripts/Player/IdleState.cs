using UnityEngine;

public class IdleState : BasePlayerState
{
    public IdleState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.meleeAttackState);

        if (Mathf.Abs(player.horizontal) > 0.01f)
            stateMachine.ChangeState(player.runningState);

        if (player.jumpBufferCounter > 0f && player.coyoteTimeCounter > 0f)
            stateMachine.ChangeState(player.jumpingState);

        if (Input.GetKeyDown(KeyCode.S))
            stateMachine.ChangeState(player.crouchingState);
    }
}
