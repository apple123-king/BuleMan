using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private bool destroyOnAnyCollision = true;

    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = direction.normalized * speed;

        if (rb != null)
        {
            rb.velocity = velocity;
        }
        else
        {
            transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
        }
    }

    public void Launch(Vector2 launchDirection, float launchSpeed)
    {
        if (launchDirection.sqrMagnitude > 0f)
        {
            direction = launchDirection.normalized;
        }

        if (launchSpeed > 0f)
        {
            speed = launchSpeed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHit(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision.gameObject);
    }

    private void HandleHit(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            HeroLife heroLife = target.GetComponent<HeroLife>();
            if (heroLife != null)
            {
                heroLife.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        if (destroyOnAnyCollision)
        {
            Destroy(gameObject);
        }
    }
}
