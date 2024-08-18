using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovementOld : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        // Make player always face the direction of gravity
        transform.up = -Physics2D.gravity.normalized;

        // Moving the actual movement to FixedUpdate would be good for optimization later
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
            
            //transform.Translate(Vector2.right * (Time.deltaTime * speed), Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(-transform.right * speed);

            //transform.Translate(Vector2.left * (Time.deltaTime * speed), Space.Self);
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = transform.up * jumpingPower;
        }

        if (Input.GetButtonUp("Jump") && Vector2.Dot(rb.velocity, transform.up) > 0)
        {
            rb.velocity = rb.velocity * 0.5f;
        }

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
}