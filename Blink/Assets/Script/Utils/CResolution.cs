using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CResolution : MonoBehaviour
{
    [SerializeField]
    private Dropdown dropdown;
    [SerializeField]
    private List<Toggle> toggles;

    private ResolutionSetting resolutionSetting;
    // Start is called before the first frame update
    void Awake()
    {
        resolutionSetting = FindObjectOfType<ResolutionSetting>();
    }

    private void OnEnable()
    {
        print(string.Format("OnEnable {0} - {1}", resolutionSetting.UserResolution.Key, resolutionSetting.UserResolution.Value));
        dropdown.value = resolutionSetting.UserResolution.Key;
        toggles[(int)resolutionSetting.UserResolution.Value].isOn = true;
    }
}
