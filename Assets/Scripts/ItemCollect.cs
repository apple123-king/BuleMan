using UnityEngine;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] public Text cherriesText;
    [SerializeField] private AudioSource collectSoundEffect;

    private void Start()
    {
        RefreshScoreText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectibleItem collectible = collision.GetComponent<CollectibleItem>();
        if (collectible != null)
        {
            collectible.ApplyBonus(gameObject);
            Collect(collision.gameObject, collectible.ScoreValue);
            return;
        }

        if (collision.gameObject.CompareTag("Cherry"))
        {
            Collect(collision.gameObject, 1);
        }
    }

    public void Collect(GameObject item, int scoreValue)
    {
        collectSoundEffect?.Play();
        GameManager.Instance.AddScore(scoreValue);
        RefreshScoreText();
        Destroy(item);
    }

    public void RefreshScoreText()
    {
        if (cherriesText == null)
        {
            return;
        }

        cherriesText.text = "Cherries: " + GameManager.Instance.score;
    }
}
