using UnityEngine;
using UnityEngine.InputSystem;


public class Player_script : MonoBehaviour
{
    public float movement;
    public Rigidbody2D rb;
    public float speed = 5f;
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
    }
}
