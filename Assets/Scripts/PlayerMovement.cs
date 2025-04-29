using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;     
    public float jumpForce = 1f;     
    private Rigidbody2D rb;           
    private bool isGrounded = true;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        if (isGrounded && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            //rotate the player
            transform.Rotate(0, 0, 90); // Rotate the player by 45 degrees
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
