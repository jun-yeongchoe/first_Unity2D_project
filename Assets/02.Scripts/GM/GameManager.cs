using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;
    public Player player;
    public Enemy enemy;

    
    private bool gameOver;
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
        if (!player.isLive)
        {
            Debug.Log("플레이어 사망");
            gameOver = true;
        }

        if (gameOver)
        {
            gameOverPanel.SetActive(true);
        }

    }


    public void OnClickRestart()
    {
        SceneManager.LoadScene("1st_Floor_Scene", LoadSceneMode.Single);
    }

    public void OnClickGoTitle()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
