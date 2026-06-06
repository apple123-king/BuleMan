using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.4f;
    [SerializeField] private float resetDelay = 3f;
    [SerializeField] private float gravityScale = 3f;
    [SerializeField] private bool resetAfterFall = true;

    private Rigidbody2D rb;
    private Collider2D[] colliders;
    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private RigidbodyType2D startBodyType;
    private float startGravityScale;
    private bool triggered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        startRotation = transform.rotation;

        if (rb != null)
        {
            startBodyType = rb.bodyType;
            startGravityScale = rb.gravityScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerFall();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerFall();
        }
    }

    private void TriggerFall()
    {
        if (!triggered)
        {
            triggered = true;
            StartCoroutine(FallRoutine());
        }
    }

    private IEnumerator FallRoutine()
    {
        yield return new WaitForSeconds(fallDelay);

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = gravityScale;
            rb.velocity = Vector2.zero;
        }
        else
        {
            SetPlatformVisible(false);
        }

        if (resetAfterFall)
        {
            yield return new WaitForSeconds(resetDelay);
            ResetPlatform();
        }
    }

    private void ResetPlatform()
    {
        transform.SetPositionAndRotation(startPosition, startRotation);

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = startBodyType;
            rb.gravityScale = startGravityScale;
        }

        SetPlatformVisible(true);
        triggered = false;
    }

    private void SetPlatformVisible(bool visible)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = visible;
        }

        foreach (Collider2D platformCollider in colliders)
        {
            platformCollider.enabled = visible;
        }
    }
}
