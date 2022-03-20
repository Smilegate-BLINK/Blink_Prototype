using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Slider sunglassSlider;

    public Image sunglassImage;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            sunglassImage.enabled = false;
            sunglassSlider.enabled = false;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnableSunglass()
    {
        sunglassSlider.value = sunglassSlider.maxValue;
        sunglassSlider.enabled = true;
        sunglassImage.enabled = true;
        StartCoroutine(DecreaseDurability());
    }

    IEnumerator DecreaseDurability()
    {
        while (sunglassSlider.value > 0)
        {
            sunglassSlider.value -= Time.deltaTime * 10;
            yield return null;
        }

        sunglassSlider.enabled = false;
        sunglassImage.enabled = false;
    }
}
