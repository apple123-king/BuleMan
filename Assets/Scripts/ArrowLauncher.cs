using System.Collections;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    [SerializeField] private ArrowProjectile arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Vector2 fireDirection = Vector2.right;
    [SerializeField] private float arrowSpeed = 8f;
    [SerializeField] private float firstShotDelay = 0.5f;
    [SerializeField] private float shotInterval = 2f;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private Animator animator;
    [SerializeField] private string shootTrigger = "Shoot";

    public void Configure(ArrowProjectile projectilePrefab, Transform point, Vector2 direction, float speed, float interval)
    {
        arrowPrefab = projectilePrefab;
        firePoint = point;
        fireDirection = direction;
        arrowSpeed = speed;
        shotInterval = interval;
    }

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(Mathf.Max(0f, firstShotDelay));

        while (enabled)
        {
            Shoot();
            yield return new WaitForSeconds(Mathf.Max(0.05f, shotInterval));
        }
    }

    public void Shoot()
    {
        if (arrowPrefab == null)
        {
            return;
        }

        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        Quaternion spawnRotation = firePoint != null ? firePoint.rotation : transform.rotation;
        ArrowProjectile arrow = Instantiate(arrowPrefab, spawnPosition, spawnRotation);
        arrow.Launch(GetWorldDirection(), arrowSpeed);

        shootSound?.Play();

        if (animator != null && !string.IsNullOrEmpty(shootTrigger))
        {
            animator.SetTrigger(shootTrigger);
        }
    }

    private Vector2 GetWorldDirection()
    {
        if (firePoint != null)
        {
            return firePoint.right;
        }

        if (fireDirection.sqrMagnitude <= 0f)
        {
            return Vector2.right;
        }

        return transform.TransformDirection(fireDirection.normalized);
    }
}
