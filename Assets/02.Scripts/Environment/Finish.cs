using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.withObject)
        {
            Debug.Log(GameManager.instance.gameOver);
            Debug.Log(MainMenu.pName);
            //if (GameManager.instance.player.hp == 0) Debug.Log("�÷��̾� HP�� Null");
            if (GameManager.instance.player == null) Debug.Log("�÷��̾ Null");
            Debug.Log(GameManager.instance.player.hp);
            Debug.Log(Timer.ElapsedSeconds);
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
        if (!Player.withObject)
        {
            Debug.Log("������ �����ϼ���.");
        }
    }
}
