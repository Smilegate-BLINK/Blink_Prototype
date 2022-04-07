using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CResolution : MonoBehaviour
{
    private Tuple<int, int>[] DefaultResolution = new Tuple<int, int>[4]
        {
            new Tuple<int, int>(960, 720),
            new Tuple<int, int>(1280, 720),
            new Tuple<int, int>(1600, 900),
            new Tuple<int, int>(1920, 1080)
        };
    private KeyValuePair<int, ScreenMode> resolutionSetting;
    [SerializeField]
    private Dropdown dropdown;
    [SerializeField]
    private List<Toggle> toggles;
    private int resolutionIndex;
    private ScreenMode screenMode;
    // Start is called before the first frame update
    void Start()
    {
        resolutionSetting = FindObjectOfType<ResolutionSetting>().UserResolution;
        dropdown.value = resolutionSetting.Key;
        toggles[(int)resolutionSetting.Value].isOn = true;
        UIManager.instance.ChangeScreenResolution(DefaultResolution[resolutionSetting.Key], resolutionSetting.Value);
    }

    private void OnEnable()
    {
        dropdown.value = resolutionSetting.Key;
        toggles[(int)resolutionSetting.Value].isOn = true;
    }

    public void ChangeResolution(int value)
    {
        resolutionIndex = value;
    }

    public void ChangeScreenMode(int value)
    {
        screenMode = (ScreenMode)value;
    }

    public void AcceptResolution()
    {
        resolutionSetting = new KeyValuePair<int, ScreenMode>(resolutionIndex, screenMode);
        UIManager.instance.ChangeScreenResolution(DefaultResolution[resolutionIndex], screenMode);
    }
}
