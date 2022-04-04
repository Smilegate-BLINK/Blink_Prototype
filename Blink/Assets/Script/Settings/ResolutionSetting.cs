using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ScreenMode { WINDOW, FULLSCREEN }

public class ResolutionSetting : MonoBehaviour
{
    private int resolutionIndex;
    private ScreenMode screenMode;
    private KeyValuePair<int, ScreenMode> userResolution;
    private Tuple<int, int>[] DefaultResolution = new Tuple<int, int>[4]
        {
            new Tuple<int, int>(960, 720),
            new Tuple<int, int>(1280, 720),
            new Tuple<int, int>(1600, 900),
            new Tuple<int, int>(1920, 1080)
        };

// Start is called before the first frame update
void Start()
    {
        var fName = string.Format("{0}/{1}.json", Application.dataPath + "/DataFiles", "ResolutionSetting");
        var jsonData = File.ReadAllText(fName);
        userResolution = JsonConvert.DeserializeObject<KeyValuePair<int, ScreenMode>>(jsonData);
        print(string.Format("{0} - {1}", userResolution.Key, userResolution.Value));
        UIManager.instance.ChangeScreenResolution(DefaultResolution[userResolution.Key], userResolution.Value);
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
        userResolution = new KeyValuePair<int, ScreenMode>(resolutionIndex, screenMode);
        UIManager.instance.ChangeScreenResolution(DefaultResolution[resolutionIndex], screenMode);
    }

    private void OnDisable()
    {
        resolutionIndex = 3;
        screenMode = ScreenMode.FULLSCREEN;
    }

    private void OnApplicationQuit()
    {
        var jsonData = JsonConvert.SerializeObject(userResolution);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.dataPath + "/DataFiles", "ResolutionSetting", jsonData);
    }
}
