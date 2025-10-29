using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.withObject)
        {
            var data = new GameData
            {
                playerName = MainMenu.name,
                hp = GameManager.instance.player.hp,
                playTime = Timer.ElapsedSeconds
            };
            SaveSystem.Save(data);
            SceneManager.LoadScene("FinishScene");
        }
        if (!Player.withObject)
        {
            Debug.Log("인질을 구출하세요.");
        }
    }
}
