using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class wheel_movement : MonoBehaviour
{
    [SerializeField] private float forceStrength = 4f;
    [SerializeField] private float restoringForce = 1.3f;
    [SerializeField] private Rigidbody2D wheelRigidbody;
    [SerializeField] private Transform playerPosition;
    private float direction;
    private float restore;
    private void Awake()
    {
        wheelRigidbody = GetComponent<Rigidbody2D>();
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

    public void PID()
    {
        Quaternion angle = transform.rotation;
        restore = angle.z / 0.7489139f;
    }
    private void FixedUpdate()
    {
        wheelRigidbody.AddForceAtPosition(direction * transform.right * forceStrength, playerPosition.position - new Vector3(0, 1f, 0));
        PID();

        Vector3 restoreForce = new Vector3(restore * restoringForce, transform.up.y, 0);
        wheelRigidbody.AddForceAtPosition(restoreForce, playerPosition.position + new Vector3(0, 1f, 0));
    }
}
