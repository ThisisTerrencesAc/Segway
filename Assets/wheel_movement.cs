using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class wheel_movement : MonoBehaviour
{
    [SerializeField] private float forceStrength = 4f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float restoringForce = 2.3f;
    [SerializeField] private Rigidbody2D wheelRigidbody;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float groundNormalThreshold = 0.5f;
    [SerializeField] private bool useGentleMovementTuning = true;
    [SerializeField] private float tapDirectionMultiplier = 0.6f;
    [SerializeField] private float holdDirectionMultiplier = 2f;
    [SerializeField] private float holdRampTime = 0.65f;
    [SerializeField] private float tapForcePointYOffset = -0.25f;
    [SerializeField] private float holdForcePointYOffset = -1f;
    private float direction;
    private float moveInput;
    private float moveHeldTime;
    private float restore;
    private readonly HashSet<Collider2D> groundColliders = new HashSet<Collider2D>();

    private bool IsGrounded => groundColliders.Count > 0;

    private void Awake()
    {
        wheelRigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        if (value.Get<Vector2>().x > 0.01f)
        {
            direction = 2f;
            moveInput = 1f;
        }

        else if (value.Get<Vector2>().x < -0.01)
        {
            direction = -2f;
            moveInput = -1f;
        }

        else
        {
            direction = 0f;
            moveInput = 0f;
        }
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed || !IsGrounded)
        {
            return;
        }

        wheelRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void PID()
    {
        Quaternion angle = transform.rotation;
        restore = angle.z / 0.7489139f;
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
            moveHeldTime = 0f;
            return;
        }

        if (!useGentleMovementTuning)
        {
            wheelRigidbody.AddForceAtPosition(direction * transform.right * forceStrength, playerPosition.position - new Vector3(0, 1f, 0));
        }
        else
        {
            if (Mathf.Abs(moveInput) > 0.01f)
            {
                moveHeldTime += Time.fixedDeltaTime;
            }
            else
            {
                moveHeldTime = 0f;
            }

            float holdAmount = holdRampTime > 0f ? Mathf.Clamp01(moveHeldTime / holdRampTime) : 1f;
            float directionMultiplier = Mathf.Lerp(tapDirectionMultiplier, holdDirectionMultiplier, holdAmount);
            float forcePointYOffset = Mathf.Lerp(tapForcePointYOffset, holdForcePointYOffset, holdAmount);
            Vector3 forcePosition = playerPosition.position + new Vector3(0f, forcePointYOffset, 0f);

            wheelRigidbody.AddForceAtPosition(moveInput * directionMultiplier * transform.right * forceStrength, forcePosition);
        }

        PID();
        Vector3 restoreForce = new Vector3(restore * restoringForce, transform.up.y, 0);
        wheelRigidbody.AddForceAtPosition(restoreForce, playerPosition.position + new Vector3(0, 1f, 0));
    }
}
