using UnityEngine;

public class RunningState : BasePlayerState
{
    public RunningState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.meleeAttackState);

        if (Mathf.Abs(player.horizontal) < 0.01f)
            stateMachine.ChangeState(player.idleState);

        if (player.jumpBufferCounter > 0f && player.coyoteTimeCounter > 0f)
            stateMachine.ChangeState(player.jumpingState);
    }

    public override void PhysicsUpdate()
    {
        player.rb.linearVelocity = new Vector2(player.horizontal * player.speed, player.rb.linearVelocity.y);
    }
}
