using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private AnimationScript anim;
    public Rigidbody2D rb { get; private set; }

    public float speed = 8f;
    public float jumpForce = 16f;
    public float runMaxSpeed = 10f;
    
    private Vector2 orientationNormal;

    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<AnimationScript>();
    }    

    void Update()
    {
        // Make player always face the direction of gravity
        transform.up = -Physics2D.gravity.normalized;

        Move();
    }

    private void Move()
    {
        // Normalize the current gravity direction
        orientationNormal = Physics2D.gravity.normalized;

        // Take in input data and adjust it based on the player's local space
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        Vector2 localMoveDirection = transform.TransformDirection(moveInput);

        // Calculate target velocity based on input and speed
        Vector2 targetVelocity = localMoveDirection * speed;

        // Project the current velocity onto the surface perpendicular to gravity (movement plane)
        Vector2 velocityOnMovePlane = rb.velocity - Vector2.Dot(rb.velocity, orientationNormal) * orientationNormal;

        // This should not have been as hard as it was to figure out fml
        Vector2 newVelocity = Vector2.Lerp(velocityOnMovePlane, targetVelocity, 0.2f); // Adjust Lerp factor as needed

        // Add gravity component *back* to the velocity
        newVelocity += Vector2.Dot(rb.velocity, orientationNormal) * orientationNormal;

        // Jump Handling
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            anim.SetTrigger("jump");
            newVelocity += (-orientationNormal) * jumpForce;
        }

        // Variable jump height handling
        if (Input.GetButtonUp("Jump") && Vector2.Dot(rb.velocity, -orientationNormal) > 0)
        {
            newVelocity += orientationNormal * rb.velocity.magnitude * 0.5f;
        }

        // Set the Rigidbody's velocity to the calculated velocity
        rb.velocity = newVelocity;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}