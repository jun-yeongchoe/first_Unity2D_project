using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.hasKey)
        {
            var data = new GameData
            {
                playerName = MainMenu.name,
                hp = GameManager.instance.player.hp,
                playTime = Timer.ElapsedSeconds
            };
            SaveSystem.Save(data);
            foreach(var e in GameObject.FindGameObjectsWithTag("Enemy")) e.SetActive(false);
            SceneManager.LoadScene("UnderGroundRoom");
        }
        if (!Player.hasKey)
        {
            Debug.Log("키가 없습니다.");
        }
    }
}
