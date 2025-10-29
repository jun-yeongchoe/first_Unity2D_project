using System.IO;
using UnityEngine;

public static class SaveSystem //게임데이터를 저장하고 JSON파일로 불러올 역할을 할 녀석
{
    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "save.json");

    // 데이터를 JSON형식으로 저장
    public static void Save(GameData data, bool prettyPrint = true)
    {
        //객체를 JSON 형식으로 저장
        string json = JsonUtility.ToJson(data, prettyPrint);

        //문자열을 파일로 저장
        File.WriteAllText(SavePath, json);

        Debug.Log("저장완료 : " + SavePath);
        Debug.Log(json);

    }
    //JSON 파일을 읽어서 객체로 돌리자
    public static bool TryLoad(out GameData data)
    {
        if (!File.Exists(SavePath)) 
        {
            data = null;
            Debug.Log("저장파일이 없다");
            return false;
        }

        string json = File.ReadAllText(SavePath);
        data = JsonUtility.FromJson<GameData>(json);
        Debug.Log("불러오기 성공 : " + SavePath);
        Debug.Log(json);

        return true;
    }
}
