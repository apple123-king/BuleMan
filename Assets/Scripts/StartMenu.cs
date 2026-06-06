using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private AudioSource click;
    public void LoadGame()
    {
        click.Play();
        GameManager.Instance.currentLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
