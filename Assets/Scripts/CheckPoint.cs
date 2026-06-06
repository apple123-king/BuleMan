using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private AudioSource checkPointSoundEffect;
    [SerializeField] private float nextLevelDelay = 5f;

    private bool isCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCompleted)
        {
            isCompleted = true;
            checkPointSoundEffect?.Play();
            GameManager.Instance.AdvanceLevel();
            Invoke(nameof(LoadNextLevel), nextLevelDelay);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
