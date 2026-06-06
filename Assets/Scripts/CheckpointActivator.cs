using UnityEngine;

public class CheckpointActivator : MonoBehaviour
{
    [SerializeField] private AudioSource activateSound;
    [SerializeField] private Animator animator;
    [SerializeField] private string activateTrigger = "Activate";
    [SerializeField] private Transform respawnPoint;

    private bool activated;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        HeroLife heroLife = collision.GetComponent<HeroLife>();
        if (heroLife == null)
        {
            return;
        }

        heroLife.SetBornPoint(respawnPoint != null ? respawnPoint.position : transform.position);

        if (!activated)
        {
            activated = true;
            activateSound?.Play();

            if (animator != null && !string.IsNullOrEmpty(activateTrigger))
            {
                animator.SetTrigger(activateTrigger);
            }
        }
    }
}
