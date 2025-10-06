using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 8f;
    public float jumpingPower = 16f;

    [Header("Glide Settings")]
    public float maxGlideTime = 2f;
    public float glideSpeed = -2f;

    [Header("Combat Settings")]
    public float attackDuration = 0.3f;
    public GameObject meleeHitbox;

    [Header("Crouch Settings")]
    

    [Header("Components")]
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator; // Optional if you have animations
    public HealthManager healthManager;

    // --- FSM ---
    public PlayerStateMachine stateMachine { get; private set; }
    public IdleState idleState { get; private set; }
    public RunningState runningState { get; private set; }
    public JumpingState jumpingState { get; private set; }
    public FallingState fallingState { get; private set; }
    public GlidingState glidingState { get; private set; }
    public MeleeAttackState meleeAttackState { get; private set; }
    public CrouchingState crouchingState { get; private set; }

    // --- Runtime vars ---
    [HideInInspector] public float horizontal;
    [HideInInspector] public float coyoteTimeCounter;
    [HideInInspector] public float jumpBufferCounter;
    [HideInInspector] public float glideTimeLeft;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isGlideInputHeld;
    [HideInInspector] public bool isCrouching;


    private float coyoteTime = 0.2f;
    private float jumpBufferTime = 0.2f;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this, stateMachine);
        runningState = new RunningState(this, stateMachine);
        jumpingState = new JumpingState(this, stateMachine);
        fallingState = new FallingState(this, stateMachine);
        glidingState = new GlidingState(this, stateMachine);
        meleeAttackState = new MeleeAttackState(this, stateMachine);
        crouchingState = new CrouchingState(this, stateMachine);
    }

    private void Start()
    {
        glideTimeLeft = maxGlideTime;
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        // --- INPUT ---
        horizontal = Input.GetAxisRaw("Horizontal");
        isGlideInputHeld = Input.GetKey(KeyCode.E);

        // Coyote time handling
        if (IsGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // Jump buffer handling
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // Melee attack input
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            stateMachine.ChangeState(meleeAttackState);
        }

        // Update FSM logic
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Poop()
    {
        // If you want to add pooping, just call Instantiate(poopProjectile, ...)
    }
}
