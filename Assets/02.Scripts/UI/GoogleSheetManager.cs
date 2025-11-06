using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    public string csvURL = "https://docs.google.com/spreadsheets/d/1wKliWH_XftOBHAu0sPX6vb9qumEbruhmRMXujSh5GM0/export?format=csv";
    private string[,] rank = new string[3, 2];
    [SerializeField] private TextMeshProUGUI[] tName;
    [SerializeField] private TextMeshProUGUI[] score;
    private int v;

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
            yield break;
        }

        string[] lines = www.downloadHandler.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');
            var ranking = new Dictionary<int, string>();
            for(int r = 0; r < lines.Length; r++)
            {
                ranking.Add(int.Parse(values[4]), lines[i]);
            }
            Dictionary<int, string> sortedRanking = ranking.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            var top3 = sortedRanking.Take(3);
            foreach (var item in top3)
            {
                Debug.Log(item.Value);
            }

            if (int.TryParse(values[4], out v) && v == 1)
            {
                for (int c = 0; c < rank.GetLength(1); c++) 
                {
                    rank[0 , c] = values[c+1];
                }
            }
            else if (int.TryParse(values[4], out v) && v == 2)
            {
                for (int c = 0; c < rank.GetLength(1); c++)
                {
                    rank[1, c] = values[c + 1];
                }
            }
            else if (int.TryParse(values[4], out v) && v == 3)
            {
                for (int c = 0; c < rank.GetLength(1); c++)
                {
                    rank[2, c] = values[c + 1];
                }
            }
            else continue;
        }
        for (int i = 0; i < tName.Length; i++) 
        {
            tName[i].text = rank[i, 0];
            score[i].text = rank[i, 1].ToString().Replace('"', ' ');
        }

    }
}
