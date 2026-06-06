using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private int Leveindex;
    [SerializeField] private AudioSource checkPointSoundEffect;
    private bool IsCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&!IsCompleted)
        {
            IsCompleted = true;
            checkPointSoundEffect.Play();
            GameManager.Instance.currentLevel++;
            Invoke("LoadNextLevel", 5f);
            //LoadNextLevel();
        }
        
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }
}
