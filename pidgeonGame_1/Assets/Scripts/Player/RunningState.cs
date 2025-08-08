using UnityEngine;

public class RunningState : BasePlayerState
{
    public RunningState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        // Optional: Set animation trigger
        // player.animator.SetBool("isRunning", true);
    }

    public override void LogicUpdate()
    {
        if (!player.IsGrounded())
        {
            stateMachine.ChangeState(player.fallingState);
        }
        else if (Mathf.Abs(player.horizontal) < 0.1f)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (player.jumpBufferCounter > 0f && player.coyoteTimeCounter > 0f)
        {
            stateMachine.ChangeState(player.jumpingState);
        }
    }

    public override void PhysicsUpdate()
    {
        player.rb.linearVelocity = new Vector2(player.horizontal * player.speed, player.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        // player.animator.SetBool("isRunning", false);
    }
}
