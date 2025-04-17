using UnityEngine;

public class Idle : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    private PlayerMovementSM playsm;
    bool crouched;

    public Idle(PlayerMovementSM playerStateMachine) : base("Idle", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    private Gun gun;

    public override void Enter()
    {
        base.Enter();
        horizontalInput = 0;
        verticalInput = 0;
        playsm.speed = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        horizontalInput = playsm.pControls.Player.Move.ReadValue<Vector2>().x;
        verticalInput = playsm.pControls.Player.Move.ReadValue<Vector2>().y;
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (playsm.pControls.Player.Move.ReadValue<Vector2>().magnitude > 0.01f && playsm.weapon.aiming == false)
        {
            playerStateMachine.ChangeState(playsm.walkingState);
            playsm.anim.SetBool("Walking", true);
            AudioManager.manager.Play("walk");
            playsm.speed = 3;
        }

        if (playsm.pControls.Player.Crouch.IsPressed() && playsm.Crouched == false)
        {
            playsm.Crouched = true;
            playerStateMachine.ChangeState(playsm.crouchingState);
            playsm.anim.SetBool("Crouching", true);
        }

        if (playsm.pControls.Player.Jump.IsPressed() && playsm.isGrounded)
        {
            playerStateMachine.ChangeState(playsm.jumpingState);
            playsm.anim.SetBool("Jump", true);
            playsm.isGrounded = false;
            playsm.Jumping = true;
        }

        if (playsm.pControls.Player.Attack.IsPressed() && playsm.weapon.gunEquipped == true)
        {
            playerStateMachine.ChangeState(playsm.firingState);
            AudioManager.manager.Play("shootGun");
            playsm.anim.SetBool("shoot", true);
            playsm.isShooting = true;
        }

        if (playsm.pControls.Player.Attack.IsPressed() && playsm.weapon.gunEquipped == false)
        {
            playerStateMachine.ChangeState(playsm.punchingState);
            playsm.isPunching = true;
            AudioManager.manager.Play("Punch");
            playsm.anim.SetBool("punching", true);
        }

        if (playsm.pControls.Player.EquipGun.IsPressed() && playsm.weapon.pressCount == 0)
        {
            playsm.weapon.ammoText.gameObject.SetActive(true);
            playsm.weapon.gun.SetActive(true);
            playsm.weapon.reticle.SetActive(true);
            playsm.weapon.pressCount = 1;
            playsm.weapon.gunEquipped = true;
            AudioManager.manager.Play("equipGun");
        }

    }
}
