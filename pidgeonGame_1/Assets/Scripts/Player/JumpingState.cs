using UnityEngine;

public class JumpingState : BasePlayerState
{
    public JumpingState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.jumpingPower);
        player.jumpBufferCounter = 0f;
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.meleeAttackState);
    }

    public override void LogicUpdate()
    {
        if (player.rb.linearVelocity.y < 0f)
            stateMachine.ChangeState(player.fallingState);
    }
}
