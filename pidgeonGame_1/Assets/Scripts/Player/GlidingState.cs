using UnityEngine;

public class GlidingState : BasePlayerState
{
    public GlidingState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        // Start gliding
        // player.animator.SetBool("isGliding", true);
    }

    public override void LogicUpdate()
    {
        if (player.IsGrounded())
        {
            player.glideTimeLeft = player.maxGlideTime;
            player.hasPooped = false;
            stateMachine.ChangeState(player.idleState);
        }
        else if (!Input.GetKey(KeyCode.E) || player.glideTimeLeft <= 0f)
        {
            stateMachine.ChangeState(player.fallingState);
        }

        player.glideTimeLeft -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.S) && player.canPoop && !player.hasPooped)
        {
            player.Poop();
        }
    }

    public override void PhysicsUpdate()
    {
        float verticalVelocity = Mathf.Max(player.rb.linearVelocity.y, player.glideSpeed);
        player.rb.linearVelocity = new Vector2(player.horizontal * player.speed, verticalVelocity);
    }

    public override void Exit()
    {
        // player.animator.SetBool("isGliding", false);
    }
}
