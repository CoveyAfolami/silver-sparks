using UnityEngine;
using System.Collections;

public class JumpingState : BasePlayerState
{
    private float jumpTime = 0.4f;
    private float elapsedTime = 0f;

    public JumpingState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.jumpingPower);
        player.StartCoroutine(JumpCooldown());
        // player.animator.SetTrigger("jump");
    }

    public override void LogicUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (player.rb.linearVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.fallingState);
        }
    }

    public override void PhysicsUpdate()
    {
        // Keep horizontal control in air
        player.rb.linearVelocity = new Vector2(player.horizontal * player.speed, player.rb.linearVelocity.y);
    }

    private IEnumerator JumpCooldown()
    {
        player.coyoteTimeCounter = 0f;
        yield return new WaitForSeconds(jumpTime);
    }
}
