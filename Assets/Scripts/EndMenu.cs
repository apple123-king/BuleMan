using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class EndMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource click;
    public void ReLoadGame()
    {   
        click.Play();
        GameManager.Instance.currentLevel = 1;
        GameManager.Instance.score = 0;
        GameManager.Instance.life = 9;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-GameManager.Instance.MaxLevel);
    }
    
}
