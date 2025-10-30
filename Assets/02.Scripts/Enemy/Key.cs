using UnityEngine;

public class Key : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.player.GainKey();
            Destroy(gameObject);
        }
    }

}