using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.withObject)
        {
            SceneManager.LoadScene("FinishScene");
        }
        if (!Player.withObject)
        {
            Debug.Log("인질을 구출하세요.");
        }
    }
}
