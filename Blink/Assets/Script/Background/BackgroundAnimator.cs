using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimator : MonoBehaviour
{
    public float deltaTime;
    public List<Sprite> backgroundImages;
    private Image backImage;
    public GameObject buttons;
    // Start is called before the first frame update
    void Start()
    {
        backImage = GetComponent<Image>();
        backImage.enabled = false;
        LogoAnimator logoAnimator = transform.parent.Find("LogoImage").GetComponent<LogoAnimator>();
        logoAnimator.EventAnimationEnd += new EventHandler(OnEnableBackground);
        logoAnimator.StartAnimation();
    }

    IEnumerator BackgroundAnimate()
    {
        int index = 0;
        backImage.enabled = true;
        while (index < backgroundImages.Count)
        {
            backImage.sprite = backgroundImages[index++];
            yield return new WaitForSeconds(deltaTime);
        }
        buttons.SetActive(true);
    }

    private void OnEnableBackground(object sender, EventArgs e)
    {
        var obj = sender as GameObject;
        obj.SetActive(false);
        StartCoroutine(BackgroundAnimate());
    }
}
