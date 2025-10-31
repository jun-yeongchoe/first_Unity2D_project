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
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        gameOver = false;
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        RebindSceneRefs();
    }

    private void RebindSceneRefs()
    {
        player = FindAnyObjectByType<Player>(FindObjectsInactive.Exclude);

        var marker = FindFirstObjectByType<GameOverPanelMarker>(FindObjectsInactive.Include);
        gameOverPanel = marker.gameObject;

        if (marker.restartButton)
        {
            marker.restartButton.onClick.RemoveAllListeners();
            marker.restartButton.onClick.AddListener(OnClickRestart);
        }

        if (marker.goTitleButton)
        {
            marker.goTitleButton.onClick.RemoveAllListeners();
            marker.goTitleButton.onClick.AddListener(OnClickGoTitle);
        }

    }

    private void Update()
    {
        if (gameOver) return;
        if (player == null) return;

    }

    public void OnPlayerDead()
    {
        gameOver = true;
        foreach (var e in GameObject.FindGameObjectsWithTag("Enemy")) e.SetActive(false);
        gameOverPanel.SetActive(true);
        
    }

    public void OnClickRestart()
    {
        gameOver = false;
        Timer.ElapsedSeconds = 0;
        SceneManager.LoadScene("1st_Floor_Scene", LoadSceneMode.Single);
    }

    public void OnClickGoTitle()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
