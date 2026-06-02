using UnityEngine;
using System.Collections;

public class FallingTile : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.25f;
    [SerializeField] private float destroyDelay = 3f;

    private Rigidbody2D rb;
    private Collider2D tileCollider;
    private bool triggered = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tileCollider = GetComponent<Collider2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggered)
            return;

        if (collision.gameObject.CompareTag("Player") ||
            collision.transform.root.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);

        // Stop the collision with the player from launching the tile upward
        tileCollider.enabled = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        Destroy(gameObject, destroyDelay);
    }
}
