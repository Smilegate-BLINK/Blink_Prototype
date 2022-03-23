using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        Slider slider = GetComponent<Slider>();
        Text text = GetComponentInChildren<Text>();
        text.text = slider.value.ToString();
        audioMixer.SetFloat(gameObject.name, slider.value * 0.4f - 30f);
    }

    public void SetVolumeText(float value)
    {
        Text text = GetComponentInChildren<Text>();
        var sound = value * 0.4f - 30f;
        if(sound == -30f)
        {
            audioMixer.SetFloat(gameObject.name, -80f);
        }
        else
        {
            audioMixer.SetFloat(gameObject.name, sound);
        }
        text.text = value.ToString();
    }
}
