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
        //file �а�, slider����� ����, text�� ����
    }

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString();
    }
}
