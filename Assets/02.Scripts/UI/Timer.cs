using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance { get; private set; }

    public bool isRunning { get; set; } = true;
    public static float ElapsedSeconds { get; set; }
    public static float preSeconds { get; set; }

    TextMeshProUGUI timeTxt;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        timeTxt = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (!isRunning) return;
        ElapsedSeconds += Time.deltaTime;
        timeTxt.text = ToMMSSMS(ElapsedSeconds);

        if (preSeconds > 0.1f)
        {
            preSeconds = 0f;
        }
    }

    string ToMMSSMS(float total)
    {
        int m = Mathf.FloorToInt(total / 60f);
        int s = Mathf.FloorToInt(total % 60f);
        int ms = Mathf.FloorToInt((total - Mathf.Floor(total)) * 1000f);

        return $"½Ã°£ : {m:00}:{s:00}:{ms:00}";
    }
}
