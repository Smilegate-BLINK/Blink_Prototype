using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private List<Text> text;
    [SerializeField]
    private List<Slider> slider;
    [HideInInspector]
    public Dictionary<string, int> volumes;

    public static string[] VolumeType = { "MASTER", "BGM", "SFX" };

    void Start()
    {
        var fName = string.Format("{0}/{1}.json", Application.dataPath + "/DataFiles", "VolumeSetting");
        var jsonData = File.ReadAllText(fName);
        volumes = new Dictionary<string, int>(JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonData));
        audioMixer.SetFloat("Master", volumes[VolumeType[0]] * 0.4f - 30f);
        audioMixer.SetFloat("BGM", volumes[VolumeType[1]] * 0.4f - 30f);
        audioMixer.SetFloat("SFX", volumes[VolumeType[2]] * 0.4f - 30f);
        for (int i = 0; i < volumes.Count; ++i)
        {
            slider[i].value = volumes[VolumeType[i]];
        }
    }

    private void Update()
    {
        for(int i = 0;i<text.Count;++i)
        {
            text[i].text = volumes[VolumeType[i]].ToString();
        }
    }
    public void SetMasterVolume(float value)
    {
        var sound = value * 0.4f - 30f;
        if(sound == -30f)
        {
            audioMixer.SetFloat("Master", -80f);
        }
        else
        {
            audioMixer.SetFloat("Master", sound);
        }
        volumes[VolumeType[0]] = (int)value;
    }

    public void SetBGMVolume(float value)
    {
        var sound = value * 0.4f - 30f;
        if (sound == -30f)
        {
            audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            audioMixer.SetFloat("BGM", sound);
        }
        volumes[VolumeType[1]] = (int)value;
    }
    public void SetSFXVolume(float value)
    {
        var sound = value * 0.4f - 30f;
        if (sound == -30f)
        {
            audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            audioMixer.SetFloat("SFX", sound);
        }
        volumes[VolumeType[2]] = (int)value;
    }

    private void OnApplicationQuit()
    {
        var jsonData = JsonConvert.SerializeObject(volumes);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.dataPath + "/DataFiles", "VolumeSetting", jsonData);
    }
}
