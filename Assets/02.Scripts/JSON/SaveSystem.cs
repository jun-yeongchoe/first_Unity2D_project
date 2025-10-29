using System.IO;
using UnityEngine;

public static class SaveSystem //���ӵ����͸� �����ϰ� JSON���Ϸ� �ҷ��� ������ �� �༮
{
    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "save.json");

    // �����͸� JSON�������� ����
    public static void Save(GameData data, bool prettyPrint = true)
    {
        //��ü�� JSON �������� ����
        string json = JsonUtility.ToJson(data, prettyPrint);

        //���ڿ��� ���Ϸ� ����
        File.WriteAllText(SavePath, json);

        Debug.Log("����Ϸ� : " + SavePath);
        Debug.Log(json);

    }
    //JSON ������ �о ��ü�� ������
    public static bool TryLoad(out GameData data)
    {
        if (!File.Exists(SavePath)) 
        {
            data = null;
            Debug.Log("���������� ����");
            return false;
        }

        string json = File.ReadAllText(SavePath);
        data = JsonUtility.FromJson<GameData>(json);
        Debug.Log("�ҷ����� ���� : " + SavePath);
        Debug.Log(json);

        return true;
    }
}
