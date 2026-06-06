using UnityEngine;

public class FanLift : MonoBehaviour
{
    [SerializeField] private Vector2 liftDirection = Vector2.up;
    [SerializeField] private float liftSpeed = 8f;
    [SerializeField] private float sidePush = 0f;
    [SerializeField] private bool clampUpwardSpeed = true;

    public void Configure(Vector2 direction, float speed, float side, bool clamp)
    {
        liftDirection = direction;
        liftSpeed = speed;
        sidePush = side;
        clampUpwardSpeed = clamp;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            return;
        }

        Vector2 direction = liftDirection.sqrMagnitude > 0f ? liftDirection.normalized : Vector2.up;
        Vector2 pushVelocity = direction * liftSpeed + Vector2.right * sidePush;

        if (clampUpwardSpeed)
        {
            float xVelocity = Mathf.Approximately(sidePush, 0f)
                ? rb.velocity.x
                : Mathf.Max(rb.velocity.x, pushVelocity.x);

            rb.velocity = new Vector2(
                xVelocity,
                Mathf.Max(rb.velocity.y, pushVelocity.y));
        }
        else
        {
            rb.velocity += pushVelocity * Time.deltaTime;
        }
    }
}
