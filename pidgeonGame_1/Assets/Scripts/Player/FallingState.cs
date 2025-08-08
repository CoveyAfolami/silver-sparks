using UnityEngine;

public class FallingState : BasePlayerState
{
    public FallingState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void LogicUpdate()
    {
        if (player.IsGrounded())
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (Input.GetKey(KeyCode.E) && player.glideTimeLeft > 0f)
        {
            stateMachine.ChangeState(player.glidingState);
        }
        else if (Input.GetKeyDown(KeyCode.S) && player.canPoop && !player.hasPooped)
        {
            player.Poop();
        }
    }

    public override void PhysicsUpdate()
    {
        player.rb.linearVelocity = new Vector2(player.horizontal * player.speed, player.rb.linearVelocity.y);
    }
}
