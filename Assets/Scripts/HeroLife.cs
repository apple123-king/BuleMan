using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroLife : MonoBehaviour
{
    [SerializeField] private Text lifeText;
    [SerializeField] private Text LevelText;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private GameObject BornPoint;
    [SerializeField] private float rebornDelay = 0.3f;
    [SerializeField] private float invincibleAfterHit = 0.8f;

    private Animator animator;
    private bool isInvincible;
    private Vector3 spawnPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (BornPoint != null)
        {
            spawnPosition = BornPoint.transform.position;
        }
        else
        {
            spawnPosition = transform.position;
        }

        EnsureLevelNumber();
        RefreshHud();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible)
        {
            return;
        }

        isInvincible = true;
        deathSoundEffect?.Play();

        if (!GameManager.Instance.SpendLife(amount))
        {
            animator?.SetTrigger("IsDeath");
            Invoke(nameof(LoadDefeat), 1f);
            return;
        }

        RefreshHud();
        animator?.SetTrigger("IsDeath");
        Invoke(nameof(ReBorn), rebornDelay);
    }

    public void AddLife(int amount)
    {
        GameManager.Instance.AddLife(amount);
        RefreshHud();
    }

    public void SetBornPoint(Vector3 position)
    {
        spawnPosition = position;
    }

    public void GrantInvincibility(float duration)
    {
        isInvincible = true;
        CancelInvoke(nameof(ClearInvincible));
        Invoke(nameof(ClearInvincible), Mathf.Max(0f, duration));
    }

    public void RefreshHud()
    {
        if (lifeText != null)
        {
            lifeText.text = "Life: " + GameManager.Instance.life;
        }

        if (LevelText != null)
        {
            LevelText.text = "Level: " + GameManager.Instance.currentLevel;
        }
    }

    private void ReBorn()
    {
        animator?.SetTrigger("IsBorn");

        transform.position = spawnPosition;

        Invoke(nameof(ClearInvincible), invincibleAfterHit);
    }

    private void ClearInvincible()
    {
        isInvincible = false;
    }

    private void LoadDefeat()
    {
        SceneManager.LoadScene(GameManager.Instance.DefeatSceneBuildIndex);
    }

    private void EnsureLevelNumber()
    {
        if (GameManager.Instance.currentLevel > 0)
        {
            return;
        }

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex >= GameManager.Instance.firstLevelBuildIndex)
        {
            GameManager.Instance.currentLevel = buildIndex - GameManager.Instance.firstLevelBuildIndex + 1;
        }
    }
}
