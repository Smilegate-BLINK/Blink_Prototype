using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CVolume : MonoBehaviour
{
    [SerializeField]
    private List<Text> text;
    [SerializeField]
    private List<Slider> slider;
    private Dictionary<string, int> volumeSetting;
    // Start is called before the first frame update
    void Start()
    {
        volumeSetting = FindObjectOfType<VolumeSetting>().Volumes;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < volumeSetting.Count; ++i)
        {
            slider[i].value = volumeSetting[VolumeSetting.VolumeType[i]];
        }
    }
}
