
using UnityEngine;

public class DataLoad : MonoBehaviour
{
    void Start()
    {
        if (SaveSystem.TryLoad(out var loaded))
        {
            GameManager.instance.player.hp = loaded.hp;
            Timer.preSeconds = loaded.playTime;
            Debug.Log($"현재체력 : {GameManager.instance.player.hp}");
        }
    }
}
