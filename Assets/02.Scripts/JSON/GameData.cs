using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 아래의 클래스를 Json처럼 저장가능한 형태로 바꿈
public class GameData
{
    public string playerName;
    public int hp;
    public float playTime;
}
