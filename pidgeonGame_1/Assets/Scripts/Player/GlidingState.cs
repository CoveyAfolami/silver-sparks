using UnityEngine;

public class GlidingState : BasePlayerState
{
    public GlidingState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.meleeAttackState);
    }

    public override void PhysicsUpdate()
    {
        player.rb.linearVelocity = new Vector2(
            player.rb.linearVelocity.x,
            Mathf.Max(player.rb.linearVelocity.y, player.glideSpeed)
        );
        player.glideTimeLeft -= Time.deltaTime;

        if (player.glideTimeLeft <= 0f || player.IsGrounded())
        {
            if (player.IsGrounded())
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallingState);
        }
    }
}
