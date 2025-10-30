using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    public string csvURL = "https://docs.google.com/spreadsheets/d/1wKliWH_XftOBHAu0sPX6vb9qumEbruhmRMXujSh5GM0/export?format=csv";
    private string[,] rank = new string[3, 2];
    [SerializeField] private TextMeshProUGUI[] tName;
    [SerializeField] private TextMeshProUGUI[] score;


    void Start()
    {
        StartCoroutine(ImportCSV());
    }

    IEnumerator ImportCSV()
    {
        UnityWebRequest www = UnityWebRequest.Get(csvURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Download failed");
            yield break;
        }

        string[] lines = www.downloadHandler.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            if (int.Parse(values[4]) == 1)
            {
                for (int c = 0; c < rank.GetLength(1); c++) 
                {
                    rank[0 , c] = values[c+1];
                }
                
            }
            else if (int.Parse(values[4]) == 2)
            {
                for (int c = 0; c < rank.GetLength(1); c++)
                {
                    rank[1, c] = values[c + 1];
                }
            }
            else if (int.Parse(values[4]) == 3)
            {
                for (int c = 0; c < rank.GetLength(1); c++)
                {
                    rank[2, c] = values[c + 1];
                }
            }
            else continue;
        }
        for (int i = 0; i < name.Length-1; i++) 
        {
            tName[i].text = rank[i, 0];
            score[i].text = rank[i, 1].ToString().Replace('"', ' ');
        }

    }
}
