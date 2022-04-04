using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;

public enum KeyAction { INTERACT, JUMP, LEFT, RIGHT, BLINK }

public class KeySetting : MonoBehaviour
{
    private Dictionary<KeyAction, KeyCode> defaultKey = new Dictionary<KeyAction, KeyCode>
    {
        { KeyAction.INTERACT, KeyCode.UpArrow },
        { KeyAction.JUMP, KeyCode.Space },
        { KeyAction.LEFT,  KeyCode.LeftArrow },
        { KeyAction.RIGHT, KeyCode.RightArrow },
        { KeyAction.BLINK,  KeyCode.D }
    };
    public Dictionary<KeyAction, KeyCode> userKey;
    private int idx;
    public List<Text> btnText;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var fName = string.Format("{0}/{1}.json", Application.dataPath + "/DataFiles", "KeySetting");
        var jsonData = File.ReadAllText(fName);
        userKey = new Dictionary<KeyAction, KeyCode>(JsonConvert.DeserializeObject<Dictionary<KeyAction, KeyCode>>(jsonData));
        idx = -1;
    }

    private void OnGUI()
    {
        Event ev = Event.current;
        if (ev.isKey)
        {
            userKey[(KeyAction)idx] = ev.keyCode;
            idx = -1;
        }
    }

    private void Update()
    {
        for(int i=0;i<btnText.Count;++i)
        {
            btnText[i].text = userKey[(KeyAction)i].ToString();
        }
    }

    public bool CheckKeyOverlap()
    {
        for (int i = 0; i < userKey.Count; ++i)
        {
            for (int j = i + 1; j < userKey.Count - 1; ++j)
            {
                //유저가 세팅한 키 중 같은 값을 가진 키가 두 개 이상인 경우
                //원래의 키 값으로 복원
                if (userKey[(KeyAction)i] == userKey[(KeyAction)j])
                {
                    RestoreDefault();
                    return true;
                }
            }
        }
        return false;
    }

    private void RestoreDefault()
    {
        foreach (var pair in defaultKey)
        {
            userKey[pair.Key] = defaultKey[pair.Key];
        }
    }

    public void ExchangeNewKey(int action)
    {
        idx = action;
    }

    private void OnApplicationQuit()
    {
        var jsonData = JsonConvert.SerializeObject(userKey, Formatting.Indented);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.dataPath + "/DataFiles", "KeySetting", jsonData);
    }

}
