using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;
    public Player player;
    public Enemy enemy;

    public bool gameOver;
    [SerializeField] GameObject gameOverPanel;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (gameOver) return;
        if (player == null) return;

    }

    public void OnPlayerDead()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        Timer.ElapsedSeconds = 0;
        
    }

    public void OnClickRestart()
    {
        gameOver = false;
        player.isLive = true;
        player.hp = 100;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void OnClickGoTitle()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
