using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;

    
    public float fallMultiplier = 2.5f; // Multiplier to the gravity the player deals with after hitting the peak of their jump
    public float lowJumpMultiplier = 2f; // If the player doesn't hold jump, multiplier to the extra gravity during before the peak.

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        // Make player always face the direction of gravity
        transform.up = -Physics2D.gravity.normalized;


        horizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2(horizontal, 0);
        Move(movement);



        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpingPower;
        }
        JumpControl(rb.velocity.y, Input.GetButton("Jump")); // Modifies jumps based on if the button is held or not. Handles gravity!




        /*if (Input.GetButtonUp("Jump") && Vector2.Dot(rb.velocity, transform.up) > 0)
        {
            rb.velocity = rb.velocity * 0.5f;
        }*/

        //Flip();
    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector2((horizontal * speed) + Physics2D.gravity.x, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        // I am keeping this because it might be useful for sprite reasons, but currently it does nothing
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Move(Vector2 movement) {
        //rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(dir.x * speed, rb.velocity.y), wallJumpLerp * Time.deltaTime);
		rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
    }

    private void JumpControl(float verticalVelocity, bool isJumpHeld) {
        if(verticalVelocity < 0) // If the player is falling
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(verticalVelocity > 0 && !isJumpHeld) // If the player is rising but isn't holding jump
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}