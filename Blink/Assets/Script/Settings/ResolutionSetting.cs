using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum ScreenMode { WINDOW, FULLSCREEN }

public class ResolutionSetting : MonoBehaviour
{
    private KeyValuePair<int, ScreenMode> userResolution;
    public KeyValuePair<int, ScreenMode> UserResolution
    {
        get => userResolution;
        private set => userResolution = value;
    }

// Start is called before the first frame update
void Start()
    {
        var fName = string.Format("{0}/{1}.json", Application.dataPath + "/DataFiles", "ResolutionSetting");
        var jsonData = File.ReadAllText(fName);
        userResolution = JsonConvert.DeserializeObject<KeyValuePair<int, ScreenMode>>(jsonData);
    }

    private void OnApplicationQuit()
    {
        var jsonData = JsonConvert.SerializeObject(userResolution);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.dataPath + "/DataFiles", "ResolutionSetting", jsonData);
    }
}
