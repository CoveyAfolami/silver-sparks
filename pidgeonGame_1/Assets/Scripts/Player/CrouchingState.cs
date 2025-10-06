using UnityEngine;

public class CrouchingState : BasePlayerState
{
    public CrouchingState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.isCrouching = true;
    }

    public override void LogicUpdate()
    {
        if (player.isCrouching)
        {
            player.boxCollider
        }
    }
        

}
