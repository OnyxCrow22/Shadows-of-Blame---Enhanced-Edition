using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerBaseState
{
    float horizontalInput;
    float verticalInput;
    float turnSmoothVelocity;
    Vector3 direction;
    Vector3 velocity;
    private PlayerMovementSM playsm;

    public Walk(PlayerMovementSM playerStateMachine) : base("Walk", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

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

        OnMove();
        OnLook();
    }

    public void OnLook()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playsm.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(playsm.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, playsm.turnSmoothTime);
        playsm.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void OnMove()
    {
        horizontalInput = playsm.pControls.Player.Move.ReadValue<Vector2>().x;
        verticalInput = playsm.pControls.Player.Move.ReadValue<Vector2>().y;
        direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playsm.cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(playsm.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, playsm.turnSmoothTime);
        playsm.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        playsm.har.Move(moveDir.normalized * playsm.speed * Time.deltaTime);
        velocity.y += playsm.gravity * Time.deltaTime;

        playsm.har.Move(velocity * Time.deltaTime);

        if (direction.magnitude <= 0.01f)
        {
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("Walking", false);
            AudioManager.manager.Stop("walk");
        }
        if (playsm.pControls.Player.Sprint.ReadValue<float>() > 0)
        {
            playerStateMachine.ChangeState(playsm.runningState);
            playsm.anim.SetBool("Sprinting", true);
            AudioManager.manager.Play("sprinting");
            AudioManager.manager.Stop("walk");
            playsm.speed = 8;
        }
    }
}
