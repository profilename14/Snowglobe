using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 8f;
    public float jumpForce = 16f;
    public float runMaxSpeed = 10f;

    public bool canMove;
    public int facing = 1;

    [Space]
    [Header("VFX")]
    public PlayerFX playerFX;

    private Vector2 orientationNormal;
    private Collision coll;
    private Rigidbody2D rb;
    private AnimationScript anim;
    private bool groundTouch;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        coll =  GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
    }    

    void Update()
    {
        // Make player always rotate based on the direction of gravity
        transform.up = -Physics2D.gravity.normalized;

        Move();

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        // Change the direction the sprite is facing
        float x = Input.GetAxis("Horizontal");

        if (x > 0)
        {
            facing = 1;
            anim.Flip(facing);
        }
        if (x < 0)
        {
            facing = -1;
            anim.Flip(facing);
        }

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
            playerFX.PlayJump();
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

        // Calculate the velocity relative to gravity
        Vector2 gravityDirection = Physics2D.gravity.normalized;
        float relativeVerticalVelocity = Vector2.Dot(rb.velocity, gravityDirection);

        // Pass the relative vertical velocity to the animation script
        anim.SetHorizontalMovement(moveInput.x, moveInput.y, -relativeVerticalVelocity);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void GroundTouch()
    {
        // Runs on the initial frame when player touches the ground

        facing = anim.sr.flipX ? -1 : 1;

        playerFX.PlayLanding();
    }
}