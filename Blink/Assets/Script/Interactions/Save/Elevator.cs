using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Item;

public class Elevator : MonoBehaviour, IInteraction
{
    private PlayerController2 myPlayer;
    private SpriteRenderer mySprite;

    public Sprite closedElevator;
    public Sprite opendElevator;

    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        myPlayer = GameObject.Find("Player").GetComponent<PlayerController2>();
        mySprite.sprite = closedElevator;
    }

    private void Update()
    {
        if (!WorldController.Instance.savePoints[myPlayer.tempSaveSpot].isFixed)
            mySprite.sprite = closedElevator;
        else
            mySprite.sprite = opendElevator;
    }

    public void Interact(GameObject target)
    {
        if (target.tag == "Player")
        {
            if (WorldController.Instance.savePoints[myPlayer.tempSaveSpot].isFixed == false)
            {
                PlayerController2 myPlayer = target.GetComponent<PlayerController2>();
                WorldController.Instance.saveSpot = myPlayer.tempSaveSpot;
                WorldController.Instance.savePoints[myPlayer.tempSaveSpot].isFixed = true;
                Debug.Log("Saved Successfully");
            }
            if (WorldController.Instance.savePoints[myPlayer.tempSaveSpot].isFixed == true && myPlayer.tempSaveSpot != WorldController.Instance.saveSpot)
            {
                WorldController.Instance.playerCanMove = false;
                Fade.Instance.FadeOut();
                myPlayer.tempSaveSpot = WorldController.Instance.saveSpot;
                Debug.Log("Move to Highest SavePoint");
                myPlayer.publicFadeIn();
            }
        }
    }
}
