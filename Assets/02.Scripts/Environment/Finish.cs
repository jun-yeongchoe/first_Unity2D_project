using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.withObject)
        {
            GameManager.instance.gameOver = true;
            var data = new GameData
            {
                playerName = MainMenu.pName,
                hp = GameManager.instance.player.hp,
                playTime = Timer.ElapsedSeconds
            };
            SaveSystem.Save(data);
            SceneManager.LoadScene("FinishScene");
        }
    }
}
