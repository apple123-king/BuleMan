using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void ReLoadGame()
    {
        click?.Play();
        GameManager.Instance.StartRun();
        SceneManager.LoadScene(GameManager.Instance.firstLevelBuildIndex);
    }
}
