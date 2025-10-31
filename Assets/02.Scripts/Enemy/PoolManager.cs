using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolManager : MonoBehaviour
{
    // �����յ��� ������ ����
    public GameObject[] prefabs;

    //Ǯ ����� �ϴ� ����Ʈ
    List<GameObject>[] pools;

    private readonly HashSet<GameObject> actives = new();

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++) 
        {
            pools[index] = new List<GameObject>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        // ���� �ٲ�� ���� ���� Ȱ�� �ν��Ͻ�/Ǯ �ܿ��� ����
        ResetAll();
    }

    public void ResetAll()
    {
        // �뿩 ��(Ȱ��) ���� �ı�(�Ǵ� SetActive(false)�� �����ϰ� �ʹٸ� �ٲ㵵 ��)
        foreach (var go in actives) if (go) go.SetActive(false);
        actives.Clear();

        // Ǯ ���� ������Ʈ�� ��� �ı� �� ����Ʈ ���
        for (int i = 0; i < pools.Length; i++)
        {
            var list = pools[i];
            for (int j = 0; j < list.Count; j++)
                if (list[j]) Destroy(list[j]);
            list.Clear();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ...������ Ǯ�� ����ִ� ���� ������Ʈ ����
        foreach (GameObject item in pools[index]) 
        {
            if (!item.activeSelf)
            {
                // ... �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // ... �� ã������?
        if(select == null)
        {
            // ... ���Ӱ� �����ؼ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

}
