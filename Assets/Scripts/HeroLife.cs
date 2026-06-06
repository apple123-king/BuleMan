using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class HeroLife : MonoBehaviour
{

    [SerializeField] private Text lifeText;
    private Animator animator;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private GameObject BornPoint;
    [SerializeField] private Text LevelText;
    private int currentLevel;
    private int life;

    private void Start()
    {   
        lifeText.color = Color.blue;
        life= GameManager.Instance.life;
        lifeText.text = "Remaining Life:" + life;

        animator= GetComponent<Animator>();
        
        currentLevel = GameManager.Instance.currentLevel;
        LevelText.color = Color.blue;
        LevelText.text = "Level:" + currentLevel;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            deathSoundEffect.Play();
            DeathAndReborn();
        }
    }
    void DeathAndReborn()
    {
        life--;
        if (life < 0)
        {
            animator.SetTrigger("IsDeath");
            Invoke("Empty", 1f);
            SceneManager.LoadScene(GameManager.Instance.MaxLevel + 2);
        }
        else
        {
            GameManager.Instance.life = life;
            lifeText.text = "Remaining Life:" + life;
            animator.SetTrigger("IsDeath");
            Invoke("ReBorn", 0.3f);
        }
    }
    void ReBorn()
    {
        animator.SetTrigger("IsBorn");
        transform.position = BornPoint.transform.position;
    }


    void Empty() { } //用于延迟时间
}

