using System.Collections;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private Collider2D boxCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private string hitTrigger = "Hit";
    [SerializeField] private string breakTrigger = "Break";
    [SerializeField] private float destroyDelay = 0.2f;

    private bool broken;

    private void Awake()
    {
        if (boxCollider == null)
        {
            boxCollider = GetComponent<Collider2D>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsHitFromBelow(collision))
        {
            Hit();
        }
    }

    private bool IsHitFromBelow(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.4f)
            {
                return true;
            }
        }

        return false;
    }

    public void Hit()
    {
        if (broken)
        {
            return;
        }

        hitPoints--;
        hitSound?.Play();

        if (animator != null && !string.IsNullOrEmpty(hitTrigger))
        {
            animator.SetTrigger(hitTrigger);
        }

        if (spriteRenderer != null && hitSprite != null)
        {
            spriteRenderer.sprite = hitSprite;
        }

        if (hitPoints <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        broken = true;

        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        if (animator != null && !string.IsNullOrEmpty(breakTrigger))
        {
            animator.SetTrigger(breakTrigger);
        }

        if (spriteRenderer != null && brokenSprite != null)
        {
            spriteRenderer.sprite = brokenSprite;
        }

        if (dropPrefab != null)
        {
            Vector3 spawnPosition = dropPoint != null ? dropPoint.position : transform.position + Vector3.up * 0.5f;
            Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
        }

        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(Mathf.Max(0f, destroyDelay));
        Destroy(gameObject);
    }
}
