using UnityEngine;

public class GoWithBrown : MonoBehaviour
{
    private Transform playerOriginalParent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOriginalParent = collision.transform.parent;
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(playerOriginalParent);
            playerOriginalParent = null;
        }
    }
}
