using UnityEngine;

public class IdleState : BasePlayerState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void LogicUpdate()
    {
        if (!player.IsGrounded())
        {
            stateMachine.ChangeState(player.fallingState);
        }
        else if (Mathf.Abs(player.horizontal) > 0.1f)
        {
            stateMachine.ChangeState(player.runningState);
        }
        else if (player.jumpBufferCounter > 0f && player.coyoteTimeCounter > 0f)
        {
            stateMachine.ChangeState(player.jumpingState);
        }
    }

    public override void PhysicsUpdate()
    {
        player.rb.linearVelocity = new Vector2(0f, player.rb.linearVelocity.y);
    }
}
