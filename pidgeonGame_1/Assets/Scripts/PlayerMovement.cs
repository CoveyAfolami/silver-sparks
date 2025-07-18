using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    public GameObject poopProjectile;
    private bool canPoop = true;
    private bool hasPooped = false;

    // Glide settings
    public float maxGlideTime = 2f;  // Total allowed glide time
    private float glideTimeLeft = 2f;
    public float glideSpeed = -2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // Glide: if not grounded and holding E
        if (!IsGrounded())
        {
            if (Input.GetKey(KeyCode.E) && glideTimeLeft >= 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, glideSpeed));
                glideTimeLeft -= Time.deltaTime;
            }
        }
        else
        {
            // Reset glide time when player lands
            glideTimeLeft = maxGlideTime;
        }

        if (IsGrounded())
        {
            hasPooped = false;
        }

        Poop();
        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bedrock"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Poop()
    {
        if (!IsGrounded() && (canPoop && !hasPooped) && Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(poopProjectile, transform.position, Quaternion.identity);
            hasPooped = true;
            
        }
    }
}
