using UnityEngine;

public class MeleeAttackState : BasePlayerState
{
    private float attackTimer;

    public MeleeAttackState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.isAttacking = true;
        attackTimer = player.attackDuration;

        // Enable hitbox
        if (player.meleeHitbox != null)
            player.meleeHitbox.SetActive(true);

        // Stop horizontal movement while attacking
        player.rb.linearVelocity = new Vector2(0f, player.rb.linearVelocity.y);
    }

    public override void LogicUpdate()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            // Transition out of melee when done
            if (player.IsGrounded())
            {
                stateMachine.ChangeState(Mathf.Abs(player.horizontal) > 0.01f
                    ? player.runningState
                    : player.idleState);
            }
            else if (player.glideTimeLeft > 0f && player.isGlideInputHeld) // <-- safer input check
            {
                stateMachine.ChangeState(player.glidingState);
            }
            else
            {
                stateMachine.ChangeState(player.fallingState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        // Ensure no horizontal motion during attack
        player.rb.linearVelocity = new Vector2(0f, player.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.isAttacking = false;

        // Always disable hitbox on exit (covers early interruptions)
        if (player.meleeHitbox != null)
            player.meleeHitbox.SetActive(false);
    }
}
