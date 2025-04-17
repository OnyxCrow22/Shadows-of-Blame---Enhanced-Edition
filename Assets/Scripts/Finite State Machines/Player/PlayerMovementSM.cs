using UnityEngine;

public class PlayerMovementSM : PlayerStateMachine
{
    public CharacterController har;
    public float speed;
    public float turnSmoothTime;
    public float gravity;
    public float jumpHeight;
    public float groundDistance;
    public LayerMask ground;
    public Animator anim;
    public Transform cam;
    public Transform groundCheck;
    public Transform player;

    // bools
    public bool Crouched;
    public bool Jumping = false;
    public bool isShooting;
    public bool isPlayerDead = false;
    public bool inVehicle = false;
    public bool isGrounded = true;
    public bool isPunching;

    public bool throwingGrenade = false;
    public bool hasThrownGrenade = false;

    public Gun weapon;
    public PlayerHealth health;
    public PunchSystem punching;

    public PlayerControls pControls;

    // States
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Walk walkingState;
    [HideInInspector]
    public Sprint runningState;
    [HideInInspector]
    public Crouch crouchingState;
    [HideInInspector]
    public Shoot firingState;
    [HideInInspector]
    public CrouchWalking crouchWalking;
    [HideInInspector]
    public Jump jumpingState;
    [HideInInspector]
    public Punch punchingState;

    private void Awake()
    {
        idleState = new Idle(this);
        walkingState = new Walk(this);
        runningState = new Sprint(this);
        crouchingState = new Crouch(this);
        firingState = new Shoot(this);
        crouchWalking = new CrouchWalking(this);
        jumpingState = new Jump(this);
        punchingState = new Punch(this);

        pControls = new PlayerControls();
    }

    void OnEnable()
    {
        pControls.Enable();
    }

    void OnDisable()
    {
        pControls.Disable();
    }

    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }
}
