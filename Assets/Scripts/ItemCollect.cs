using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class ItemCollect : MonoBehaviour
{

    private int Cherries;
    [SerializeField] public Text cherriesText;
    [SerializeField] private AudioSource collectSoundEffect;


    private void Start()
    {
        Cherries = GameManager.Instance.score;
        cherriesText.color = Color.blue;
        cherriesText.text = "Cherries: " + Cherries;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {   
            collectSoundEffect.Play();  
            Destroy(collision.gameObject);
            Cherries++;
            cherriesText.text = "Cherries: " + Cherries;
            GameManager.Instance.score = Cherries;


        }
    }

}
