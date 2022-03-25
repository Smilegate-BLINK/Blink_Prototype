using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public PlayerInfo()
    {
    }
    public PlayerInfo(PlayerController2 player)
    {
        position = player.gameObject.transform.position;
        rotation = player.gameObject.transform.rotation;
    }
    public string ObjectToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public T JsonToObject<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
