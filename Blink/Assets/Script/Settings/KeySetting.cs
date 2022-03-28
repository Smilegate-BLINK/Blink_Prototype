using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum KeyAction { INTERACT, JUMP, LEFT, RIGHT, BLINK }

[Serializable]
public class KeySetting : MonoBehaviour
{
    private Dictionary<KeyAction, KeyCode> defaultKey = new Dictionary<KeyAction, KeyCode>
    {
        { KeyAction.JUMP, KeyCode.Space },
        { KeyAction.LEFT,  KeyCode.LeftArrow },
        { KeyAction.RIGHT, KeyCode.RightArrow },
        { KeyAction.BLINK,  KeyCode.D },
        { KeyAction.INTERACT, KeyCode.UpArrow }
    };
    public Dictionary<KeyAction, KeyCode> userKey;
    private int idx;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        idx = -1;
        userKey = new Dictionary<KeyAction, KeyCode>(defaultKey);
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

    public bool CheckKeyOverlap()
    {
        for (int i = 0; i < userKey.Count; ++i)
        {
            for (int j = i + 1; j < userKey.Count - 1; ++j)
            {
                //������ ������ Ű �� ���� ���� ���� Ű�� �� �� �̻��� ���
                //������ Ű ������ ����
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
}
