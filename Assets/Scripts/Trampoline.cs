using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private float bounceForce = 10f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim?.SetTrigger("Bounce");

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            }
        }
    }
}
