using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSpriteController : MonoBehaviour
{
    private SpriteRenderer[] allChildren;
    [HideInInspector]
    public WorldController myWorld;

    public Material eyeOpenMat;
    public Material eyeCloseMat;

    [SerializeField]
    private bool isChanged;

    // Start is called before the first frame update
    void Start()
    {
        allChildren = GetComponentsInChildren<SpriteRenderer>();
        isChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (myWorld.getWorldBlackOut())
        {
            if (myWorld.getWorldAlpha() != 0f)
            {
                foreach (SpriteRenderer sprite in allChildren)
                {
                    Color tmp = sprite.color;
                    tmp.a = myWorld.getWorldAlpha();
                    sprite.material = eyeCloseMat;
                    sprite.color = tmp;
                }
            }
            isChanged = false;
        }
        else
        {
            if (!isChanged)
            {
                foreach (SpriteRenderer sprite in allChildren)
                {
                    Color tmp = sprite.color;
                    sprite.material = eyeOpenMat;
                    tmp.a = 1f;
                    sprite.color = tmp;
                    isChanged = true;
                }
            }
        }
    }
}
