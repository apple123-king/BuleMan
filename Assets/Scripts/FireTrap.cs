using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float activeTime = 1.5f;
    [SerializeField] private float inactiveTime = 1.5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private bool startsActive = true;
    [SerializeField] private Collider2D damageCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private string activeBool = "IsActive";

    private bool isActive;
    private Coroutine toggleCoroutine;

    private void Awake()
    {
        if (damageCollider == null)
        {
            damageCollider = GetComponent<Collider2D>();
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

    private void OnEnable()
    {
        SetActiveState(startsActive);
        toggleCoroutine = StartCoroutine(ToggleRoutine());
    }

    private void OnDisable()
    {
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
            toggleCoroutine = null;
        }
    }

    private IEnumerator ToggleRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(isActive ? activeTime : inactiveTime);
            SetActiveState(!isActive);
        }
    }

    private void SetActiveState(bool active)
    {
        isActive = active;

        if (damageCollider != null)
        {
            damageCollider.enabled = active;
        }

        if (spriteRenderer != null)
        {
            if (active && activeSprite != null)
            {
                spriteRenderer.sprite = activeSprite;
            }
            else if (!active && inactiveSprite != null)
            {
                spriteRenderer.sprite = inactiveSprite;
            }
        }

        if (animator != null && !string.IsNullOrEmpty(activeBool))
        {
            animator.SetBool(activeBool, active);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            DamagePlayer(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            DamagePlayer(collision.gameObject);
        }
    }

    private void DamagePlayer(GameObject target)
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
    }
}
