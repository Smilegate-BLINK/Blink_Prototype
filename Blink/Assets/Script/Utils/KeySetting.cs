using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySetting : MonoBehaviour
{
    public Dictionary<string, KeyCode> keySettingDic = new Dictionary<string, KeyCode>
    {
        { "jump", KeyCode.Space },
        { "left",   KeyCode.LeftArrow },
        { "right", KeyCode.RightArrow },
        { "blink",  KeyCode.D },
        { "interact", KeyCode.UpArrow }
    };

    public KeyCode GetCurrentKey()
    {
        foreach(var pair in keySettingDic)
        {
            if(Input.GetKey(pair.Key))
            {
                return pair.Value;
            }
        }
        return KeyCode.None;
    }

    public KeyCode GetCurrentKeyDown()
    {
        foreach (var pair in keySettingDic)
        {
            if (Input.GetKeyDown(pair.Key))
            {
                return pair.Value;
            }
        }
        return KeyCode.None;
    }

    public KeyCode GetCurrentKeyUp()
    {
        foreach (var pair in keySettingDic)
        {
            if (Input.GetKeyUp(pair.Key))
            {
                return pair.Value;
            }
        }
        return KeyCode.None;
    }
}
