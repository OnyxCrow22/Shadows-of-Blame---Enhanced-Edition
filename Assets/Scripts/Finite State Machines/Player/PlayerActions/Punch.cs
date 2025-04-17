using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Punch(PlayerMovementSM playerStateMachine) : base("Punch", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!playsm.pControls.Player.Attack.IsPressed() && !playsm.weapon.gunEquipped)
        {
            AudioManager.manager.Stop("Punch");
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("punching", false);
            playsm.isPunching = false;
        }
    }
}
