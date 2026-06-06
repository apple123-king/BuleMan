using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private bool destroyOnPlayerHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDamage(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDamage(collision.gameObject);
    }

    private void TryDamage(GameObject target)
    {
        if (target == null || !target.CompareTag("Player"))
        {
            return;
        }

        HeroLife heroLife = target.GetComponent<HeroLife>();
        if (heroLife != null)
        {
            heroLife.TakeDamage(damage);
        }

        if (destroyOnPlayerHit)
        {
            Destroy(gameObject);
        }
    }
}
