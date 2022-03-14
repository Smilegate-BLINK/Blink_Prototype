using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private WorldController myWorld;
    private SpriteRenderer mySprite;

    public Color eyeOpendColor = Color.black;
    public Color eyeClosedColor = Color.white;

    private float startshadedTime;
    private float shadedTimeTaken;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        myWorld = GameObject.Find("World").GetComponent<WorldController>();
        mySprite = GetComponent<SpriteRenderer>();
        time = 0f;
        eyeOpendColor.a = 1f;
        eyeClosedColor.a = 1f;
        startshadedTime = myWorld.getStartshadedTime();
        shadedTimeTaken = myWorld.getShadedTimeTaken();
}

    // Update is called once per frame
    void Update()
    {
        if (myWorld.doBlinkFunc)
        {
            // ���� ����������
            if (myWorld.getWorldBlackOut())
            {
                mySprite.color = eyeClosedColor;
                time += Time.deltaTime;
                if (time > shadedTimeTaken + startshadedTime)
                    mySprite.color = eyeOpendColor;
                else if (time > startshadedTime)
                    mySprite.color = Color.Lerp(eyeClosedColor, eyeOpendColor, (time - startshadedTime) / shadedTimeTaken);
            }
            else
            {
                mySprite.color = Color.Lerp(eyeOpendColor, eyeClosedColor, time / 0.5f);
                time = 0f;
            }
        }
        else
            mySprite.color = eyeClosedColor;
    }
}
