using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FinishScene : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbxe6XUrCnTnFOJUCyYas93qZD86wrFf_-0wBXuJ9GRsQH6FP9n-zZkGuohkx6JlsxIx/exec";
    public string csvURL = "https://docs.google.com/spreadsheets/d/1wKliWH_XftOBHAu0sPX6vb9qumEbruhmRMXujSh5GM0/export?format=csv";
    int lastRow;

    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Score;
    void Start()
    {
        if (SaveSystem.TryLoad(out var loaded))
        {
            Name.text = loaded.playerName;
            Score.text = ToMMSSMS(loaded.playTime);

            AppendCsv(loaded.playerName, loaded.playTime);
            StartCoroutine(Register(loaded.playerName, loaded.playTime));
        }
    }

    string ToMMSSMS(float total)
    {
        int m = Mathf.FloorToInt(total / 60f);
        int s = Mathf.FloorToInt(total % 60f);
        int ms = Mathf.FloorToInt((total - Mathf.Floor(total)) * 1000f);

        return $"{m:00}:{s:00}:{ms:00}";
    }

    private void AppendCsv(string pName, float pTime)
    {
        string path = Path.Combine(Application.persistentDataPath, "records.csv");

        bool exists = File.Exists(path);

        using (var sw = new StreamWriter(path, append: true))
        {
            if (!exists) sw.WriteLine("�ð�, �̸�, ����(�ð�), �ɸ��ð�(��)");

            string nameEsc = EscapeCsv(pName);

            string formatted = ToMMSSMS(pTime);
            

            sw.WriteLine($"{System.DateTime.UtcNow:yyyy-MM-dd HH:mm:ss},\"{nameEsc}\", \"{formatted}\", {pTime}");
        }
        Debug.Log($"CSV ���� �Ϸ� {path}");
    }
    private string EscapeCsv(string s)
    {
        if (string.IsNullOrEmpty(s)) return ""; // Name ĭ�� ���������(null) ������ �ֵ���ǥ �ΰ��� ��ȯ
        return s.Replace("\"", "\"\"");     // �ֵ���ǥ�� �ϳ��� ���Ǿ������� �ΰ��� ��ü
    }

    IEnumerator GetLastRow()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();

            var number = www.downloadHandler.text?.Trim();
            lastRow = int.Parse(number);
        }
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) 
        {
            yield return www.SendWebRequest();

            if (www.isDone) print(www.downloadHandler.text);
            else print("���� ������ �����ϴ�.");
        }
    }

    IEnumerator Register(string pName, float pTime)
    {
        yield return StartCoroutine(GetLastRow());

        int newRow = lastRow + 1;

        WWWForm form = new WWWForm();
        
        form.AddField("realtime", $"{System.DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
        form.AddField("name", pName);
        form.AddField("score", ToMMSSMS(pTime)+"\"");
        form.AddField("truescore", pTime.ToString());
        form.AddField("rank", $"=RANK(D{newRow},D:D,1)");

        yield return StartCoroutine(Post(form));
    }
}
