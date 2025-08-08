using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject poopProjectile;
    public HealthManager healthManager;

    [Header("Movement Settings")]
    public float speed = 8f;
    public float jumpingPower = 16f;
    public float glideSpeed = -2f;
    public float maxGlideTime = 2f;

    [Header("Jump Timers")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    [HideInInspector] public float horizontal;
    [HideInInspector] public float glideTimeLeft;
    [HideInInspector] public float coyoteTimeCounter;
    [HideInInspector] public float jumpBufferCounter;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public bool hasPooped = false;
    [HideInInspector] public bool canPoop = true;

    public PlayerStateMachine stateMachine;

    public IdleState idleState;
    public RunningState runningState;
    public JumpingState jumpingState;
    public FallingState fallingState;
    public GlidingState glidingState;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this, stateMachine);
        runningState = new RunningState(this, stateMachine);
        jumpingState = new JumpingState(this, stateMachine);
        fallingState = new FallingState(this, stateMachine);
        glidingState = new GlidingState(this, stateMachine);
    }

    private void Start()
    {
        glideTimeLeft = maxGlideTime;
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        HandleTimers();
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();
        Flip();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    private void HandleTimers()
    {
        if (IsGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void Poop()
    {
        if (poopProjectile != null)
        {
            Instantiate(poopProjectile, transform.position, Quaternion.identity);
            hasPooped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bedrock"))
        {
            healthManager?.Die();
        }
    }
}
