using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private WorldController myWorld;
    private SpriteRenderer mySprite;

    public Color eyeOpendColor = Color.black;
    public Color eyeClosedColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        myWorld = GameObject.Find("World").GetComponent<WorldController>();
        mySprite = GetComponent<SpriteRenderer>();
        eyeOpendColor.a = 1f;
        eyeClosedColor.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp;

        if (myWorld.getWorldBlackOut())
        {
            tmp = eyeClosedColor;
            tmp.a = myWorld.getWorldAlpha();
            mySprite.color = tmp;
        }
        else
        {
            tmp = eyeOpendColor;
            tmp.a = 1f;
            mySprite.color = tmp;
        }
    }
}
