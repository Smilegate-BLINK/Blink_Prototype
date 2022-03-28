using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public enum VolumeType { MASTER, BGM, SFX }

[Serializable]
public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    public float masterVol
    {
        get => masterVol;
        private set => masterVol = value;
    }
    public float bgmVol
    {
        get => bgmVol;
        private set => bgmVol = value;
    }
    public float sfxVol
    {
        get => sfxVol;
        private set => sfxVol = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.instance.fileIOHelper.LoadJsonFile<VolumeSetting>(Application.dataPath + "/DataFiles", "VolumeSetting");
        audioMixer.SetFloat("Master", masterVol * 0.4f - 30f);
        audioMixer.SetFloat("BGM", bgmVol * 0.4f - 30f);
        audioMixer.SetFloat("SFX", sfxVol * 0.4f - 30f);
    }

    public void SetMasterVolumeText(float value)
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
        masterVol = sound;
    }

    public void SetBGMVolumeText(float value)
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
        bgmVol = sound;
    }
    public void SetSFXVolumeText(float value)
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
        sfxVol = sound;
    }

    private void OnApplicationQuit()
    {
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.dataPath + "/DataFiles", "VolumeSetting", JsonUtility.ToJson(this));
    }
}
