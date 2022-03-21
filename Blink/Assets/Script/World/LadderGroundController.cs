using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGroundController : MonoBehaviour
{
    private BoxCollider2D myCol;
    public GameObject myPlayer;
    private float overPosY;
    private float underPosY;

    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<BoxCollider2D>();
        myCol.enabled = false;
        overPosY = transform.position.y + (transform.localScale.y + myPlayer.transform.localScale.y) / 2;
        underPosY = transform.position.y - (transform.localScale.y + myPlayer.transform.localScale.y) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myCol.enabled)
        {
            if (myPlayer.transform.position.y > overPosY)
                myCol.enabled = true;
        }
        else
        {
            if (myPlayer.transform.position.y < underPosY)
                myCol.enabled = false;
        }
    }
}
