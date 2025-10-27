
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    [SerializeField] Tilemap ground;
    [SerializeField] int tries = 64;
    [SerializeField] int enemyCount = 20;

    int enemyPrefabs;


    private void Start()
    {
        
        enemyPrefabs = GameManager.instance.pool.prefabs.Length;
        int KeyHolder = Random.Range(0, enemyCount);
        for (int i = 0; i < enemyCount; i++) 
        {
            var enemy = GameManager.instance.pool.Get(Random.Range(0, enemyPrefabs));
            enemy.transform.position = GetRandomPointOnGroud();
            
            var e = enemy.GetComponentInChildren<Enemy>(true);
            if (i == KeyHolder && e != null)
            {
                e.hasKey = true;
            }
            e.patrolPos = GetRandomPointOnGroud();
        }
    }

    public Vector3 GetRandomPointOnGroud() // ������ Tile�� ������ ��ġ�� ã��
    {
        var b = ground.cellBounds;
        for (int i = 0; i < tries; i++)
        {
            var cell = new Vector3Int(Random.Range(b.xMin, b.xMax),
                Random.Range(b.yMin, b.yMax), 0);
            if (!ground.HasTile(cell)) continue;    // Ÿ���� ���� ĭ�� PASS
            return ground.GetCellCenterWorld(cell); // Ÿ�� �߽� ���� ��ǥ
        }

        return ground.GetCellCenterWorld(ground.WorldToCell(transform.position)); // ���� �� ������ ��ġ ��ó�� ����
    }
}
