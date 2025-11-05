using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // StartScene
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI nameStartReq;
    [SerializeField] TextMeshProUGUI invalidName;
    public TMP_InputField playerNameInput;
    public static string pName;

    private void Awake()
    {
        if (!playerNameInput) playerNameInput = GetComponentInChildren<TMP_InputField>(true);

        if (!nameStartReq) nameStartReq = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnClikInput();
        }
    }

    //TitleScene
    public void OnClickNewGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickScore()
    {
        SceneManager.LoadScene("ScoreBoard");
    }
    public void OnClickQuit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    //ScoreBoard
    public void OnClickReturn()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //StartScene
    public void OnClikInput()
    {

        if (string.IsNullOrWhiteSpace(playerNameInput.text))
        {
            invalidName.gameObject.SetActive(true);
        }
        else
        {
            nameStartReq.text = $"이름 \"{playerNameInput.text}\"(으)로 시작합니다.";
            pName = playerNameInput.text;
            invalidName.gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }

    public void OnClikStart()
    {
        var data = new GameData
        {
            playerName = pName,
            playTime = 0
        };
        SaveSystem.Save(data);
        SceneManager.LoadScene("1st_Floor_Scene");

        Timer.ElapsedSeconds = 0;
        GameManager.instance.gameOver = false;
    }
    public void OnClikRename()
    {
        playerNameInput.text = "";
        panel.SetActive(false);
    }
}
