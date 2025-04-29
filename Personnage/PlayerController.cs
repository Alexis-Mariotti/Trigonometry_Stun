using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;      
    public float jumpForce = 12f;     
    private Rigidbody2D rb;          
    private bool isGrounded = true;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);


        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
