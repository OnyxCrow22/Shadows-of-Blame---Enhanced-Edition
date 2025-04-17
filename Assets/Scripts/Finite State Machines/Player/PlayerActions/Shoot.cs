
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : PlayerBaseState
{
    private PlayerMovementSM playsm;

    public Shoot(PlayerMovementSM playerStateMachine) : base("Shoot", playerStateMachine)
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

        if (!playsm.pControls.Player.Attack.IsPressed() && !playsm.weapon.aiming)
        {
            AudioManager.manager.Stop("shootGun");
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("shoot", false);
            playsm.isShooting = false;
        }
        
        if (!playsm.pControls.Player.Attack.IsPressed() && playsm.weapon.aiming)
        {
            AudioManager.manager.Stop("shootGun");
            playsm.anim.SetBool("shoot", false);
            playsm.isShooting = false;
        }
    }
}
