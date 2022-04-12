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
        StartCoroutine(BackgroundAnimate());
    }

    IEnumerator BackgroundAnimate()
    {
        int index = 0;
        while (index < backgroundImages.Count)
        {
            backImage.sprite = backgroundImages[index++];
            yield return new WaitForSeconds(deltaTime);
        }
        buttons.SetActive(true);
    }
}
