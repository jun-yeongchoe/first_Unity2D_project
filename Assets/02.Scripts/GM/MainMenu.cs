using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    // StartScene
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI nameStartReq;
    [SerializeField] TextMeshProUGUI invalidName;
    public TMP_InputField playerNameInput;
    public static string name;

    private void Awake()
    {
        if (!playerNameInput) playerNameInput = GetComponentInChildren<TMP_InputField>(true);

        if (!nameStartReq) nameStartReq = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClikInput();
        }
    }

    //TitleScene
    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickScore()
    {
        Debug.Log("점수판");
        SceneManager.LoadScene("ScoreBoard");
    }
    public void OnClickQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    //ScoreBoard
    public void OnClickReturn()
    {
        Debug.Log("뒤로가기");
        SceneManager.LoadScene("TitleScene");
    }

    //StartScene
    public void OnClikInput()
    {

        if (string.IsNullOrWhiteSpace(playerNameInput.text))
        {
            Debug.Log("잘못된 이름을 입력하였습니다.");
            invalidName.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("이름을 입력하였습니다.");
            nameStartReq.text = $"이름 \"{playerNameInput.text}\"(으)로 시작합니다.";
            name = playerNameInput.text;
            invalidName.gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }

    public void OnClikStart()
    {
        Debug.Log("게임을 시작합니다.");
        var data = new GameData
        {
            playerName = name,
            playTime = 0
        };
        SaveSystem.Save(data);
        SceneManager.LoadScene("1st_Floor_Scene");
    }
    public void OnClikRename()
    {
        Debug.Log("이름을 다시 입력합니다.");
        playerNameInput.text = "";
        panel.SetActive(false);
    }
}
