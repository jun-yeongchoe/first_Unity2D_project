using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using UnityEngine;

public class FinishScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Score;
    void Start()
    {
        if (SaveSystem.TryLoad(out var loaded))
        {
            Name.text = loaded.playerName;
            Score.text = ToMMSSMS(loaded.playTime);

            AppendCsv(loaded.playerName, loaded.playTime);
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
            if (!exists) sw.WriteLine("시간, 이름, 점수(시간)");

            string nameEsc = EscapeCsv(pName);

            string formatted = ToMMSSMS(pTime);

            sw.WriteLine($"{System.DateTime.UtcNow:yyyy-MM-dd HH:mm:ss},\"{nameEsc}\", {formatted}\"");
        }
        Debug.Log($"CSV 저장 완료 {path}");
    }
    private string EscapeCsv(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";
        return s.Replace("\"", "\"\"");
    }
}
