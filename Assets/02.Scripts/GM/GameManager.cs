using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;
    public Player player;
    public Enemy enemy;

    private bool gameOver;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!player.isLive)
        {
            Debug.Log("플레이어 사망");
            gameOver = true;
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}
