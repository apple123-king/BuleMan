using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void LoadGame()
    {
        click?.Play();
        GameManager.Instance.StartRun();
        SceneManager.LoadScene(GameManager.Instance.firstLevelBuildIndex);
    }
}
