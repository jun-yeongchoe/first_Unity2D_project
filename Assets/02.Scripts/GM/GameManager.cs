using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;
    public Player player;
    public Enemy enemy;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!player.isLive) 
        {
            
        }
    }
}
