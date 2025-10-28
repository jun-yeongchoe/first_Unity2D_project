using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.hasKey)
        {
            SceneManager.LoadScene("UnderGroundRoom");
        }
        if (!Player.hasKey)
        {
            Debug.Log("키가 없습니다.");
        }
    }
}
