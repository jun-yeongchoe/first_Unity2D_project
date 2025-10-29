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
        Debug.Log("�� ����");
        SceneManager.LoadScene("StartScene");
    }

    public void OnClickScore()
    {
        Debug.Log("������");
        SceneManager.LoadScene("ScoreBoard");
    }
    public void OnClickQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    //ScoreBoard
    public void OnClickReturn()
    {
        Debug.Log("�ڷΰ���");
        SceneManager.LoadScene("TitleScene");
    }

    //StartScene
    public void OnClikInput()
    {

        if (string.IsNullOrWhiteSpace(playerNameInput.text))
        {
            Debug.Log("�߸��� �̸��� �Է��Ͽ����ϴ�.");
            invalidName.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("�̸��� �Է��Ͽ����ϴ�.");
            nameStartReq.text = $"�̸� \"{playerNameInput.text}\"(��)�� �����մϴ�.";
            name = playerNameInput.text;
            invalidName.gameObject.SetActive(false);
            panel.SetActive(true);
        }
    }

    public void OnClikStart()
    {
        Debug.Log("������ �����մϴ�.");
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
        Debug.Log("�̸��� �ٽ� �Է��մϴ�.");
        playerNameInput.text = "";
        panel.SetActive(false);
    }
}
