using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class wheel_movement : MonoBehaviour
{
    [SerializeField] private float forceStrength = 4f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float restoringForce = 2f;
    [SerializeField] private float proportionalGain = 0.3f;
    [SerializeField] private float derivativeGain = 0.05f;
    [SerializeField] private Rigidbody2D wheelRigidbody;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float groundNormalThreshold = 0.5f;
    private float direction;
    private float lastGroundedTime = Mathf.NegativeInfinity;
    private float lastJumpTime = Mathf.NegativeInfinity;
    private float restore;
    private readonly HashSet<Collider2D> groundColliders = new HashSet<Collider2D>();

    private bool IsGrounded => groundColliders.Count > 0;

    private Animator playerAnim;
    [SerializeField] private AudioClip jumpSFX;

    void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }


    private void Awake()
    {
        wheelRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerAnim != null)
        {
            playerAnim.SetBool("IsMoving", Mathf.Abs(wheelRigidbody.linearVelocity.x) > 0.01f);
        }

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryJump();
        }
    }

    public void OnMove(InputValue value)
    {
        if (value.Get<Vector2>().x > 0.01f)
        {
            direction = 2f;
        }

        else if (value.Get<Vector2>().x < -0.01)
        {
            direction = -2f;
        }

        else
        {
            direction = 0f;
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            TryJump();
        }
    }

    private void TryJump()
    {
        if (Time.time - lastJumpTime <= Time.fixedDeltaTime)
        {
            return;
        }

        if (Time.time - lastGroundedTime > coyoteTime)
        {
            return;
        }

        lastJumpTime = Time.time;
        lastGroundedTime = Mathf.NegativeInfinity;
        wheelRigidbody.linearVelocity = new Vector2(wheelRigidbody.linearVelocity.x, Mathf.Max(wheelRigidbody.linearVelocity.y, 0f));
        wheelRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (jumpSFX != null && AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(jumpSFX);
        }
    }

    public void PID()
    {
        float signedTiltAngle = Vector2.SignedAngle(Vector2.up, transform.up);
        float tiltError = signedTiltAngle / 90f;

        float proportional = tiltError * proportionalGain;
        float derivative = wheelRigidbody.angularVelocity * derivativeGain;
        restore = proportional + derivative;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateGroundedCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        UpdateGroundedCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundColliders.Remove(collision.collider);
    }

    private void UpdateGroundedCollision(Collision2D collision)
    {
        if (HasGroundContact(collision))
        {
            groundColliders.Add(collision.collider);
            lastGroundedTime = Time.time;
        }
        else
        {
            groundColliders.Remove(collision.collider);
        }
    }

    private bool HasGroundContact(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y >= groundNormalThreshold)
            {
                return true;
            }
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (!IsGrounded)
        {
            return;
        }

        lastGroundedTime = Time.time;
        wheelRigidbody.AddForceAtPosition(direction * transform.right * forceStrength, playerPosition.position - new Vector3(0, 1f, 0));
        PID();
        Vector3 restoreForce = new Vector3(restore * restoringForce, transform.up.y, 0);
        wheelRigidbody.AddForceAtPosition(restoreForce, playerPosition.position + new Vector3(0, 1f, 0));
    }
}
