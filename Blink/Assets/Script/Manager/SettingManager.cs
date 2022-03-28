using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingManager: MonoBehaviour
{
    public static SettingManager instance = null;
    [HideInInspector]
    public KeySetting keySetting;
    [HideInInspector]
    public VolumeSetting volumeSetting;
    [HideInInspector]
    public ResolutionSetting resolutionSetting;

    private void Start()
    {
        if(instance == null)
        {
            Init();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        //FileIO로 모든 세팅값 불러오기
        keySetting = FindObjectOfType<KeySetting>();
        volumeSetting = FindObjectOfType<VolumeSetting>();
        resolutionSetting = FindObjectOfType<ResolutionSetting>();
    }

    private void OnApplicationQuit()
    {
        //모든 세팅값 FIle에 저장
    }
}

