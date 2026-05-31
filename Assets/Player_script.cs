using UnityEngine;
using UnityEngine.InputSystem;


public class Player_script : MonoBehaviour
{
    public float movement;
    public Rigidbody2D rb;
    public float speed = 5f;
    public float jumpHeight = 7f;
    public bool isGround = true;
    void Start()
    {
        
    }

    void Update()
    {
        movement = 0f;

        if (Keyboard.current.leftArrowKey.isPressed)
            movement = -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            movement = 1f;
        rb.linearVelocity = new Vector2(movement * speed, rb.linearVelocity.y);
        if (Keyboard.current.spaceKey.isPressed && isGround)
        {
            Jump();
            //animator.SetBool("Jump",true);
            isGround = false;
        }

    }
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
    }
        void OnCollisionStay2D(Collision2D other)
    {   
        if (other.collider.CompareTag("Ground"))
            isGround = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
            isGround = false;
    }
}

