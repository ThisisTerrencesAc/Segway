using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

[RequireComponent(typeof(Rigidbody2D))]
public class wheel_movement : MonoBehaviour
{
    [SerializeField] private float forceStrength = 10f;
    [SerializeField] private float restoringTorque = 3f;
    private Rigidbody2D wheelRigidbody;
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
            direction = 3f;
        }

        else if (value.Get<Vector2>().x < -0.01)
        {
            direction = -3f;
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
        wheelRigidbody.AddForceAtPosition(direction * transform.right * forceStrength, transform.position - new Vector3(0, 0.75f, 0));
        PID();
        wheelRigidbody.AddTorque(restore * restoringTorque);
        Debug.Log(restore * restoringTorque);

    }
}
