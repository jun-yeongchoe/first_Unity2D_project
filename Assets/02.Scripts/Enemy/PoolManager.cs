using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolManager : MonoBehaviour
{
    // 프리팹들을 보관할 변수
    public GameObject[] prefabs;

    //풀 담당을 하는 리스트
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
        // 씬이 바뀌면 이전 씬의 활성 인스턴스/풀 잔여물 정리
        ResetAll();
    }

    public void ResetAll()
    {
        // 대여 중(활성) 전부 비활성화
        foreach (var go in actives) if (go) go.SetActive(false);
        actives.Clear();
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ...선택한 풀에 놀고있는 게임 오브젝트 접근
        foreach (GameObject item in pools[index]) 
        {
            if (!item.activeSelf)
            {
                // ... 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // ... 못 찾았으면?
        if(select == null)
        {
            // ... 새롭게 생성해서 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

}
