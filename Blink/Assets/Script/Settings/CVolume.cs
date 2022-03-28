using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CVolume : MonoBehaviour
{
    public float value;
    public Slider slider;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        //file 읽고, slider밸류로 설정, text도 변경
    }

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString();
    }
}
