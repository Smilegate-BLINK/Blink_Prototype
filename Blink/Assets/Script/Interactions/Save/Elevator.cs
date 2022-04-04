using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Item;

public class Elevator : MonoBehaviour, IInteraction
{
    public void Interact(GameObject target)
    {
        if (target.tag == "Player")
        {
            PlayerController2 myPlayer = target.GetComponent<PlayerController2>();
            //GameManager.instance.SetSavepoint(gameObject);
            WorldController.Instance.saveSpot = myPlayer.tempSaveSpot;
            Debug.Log("Saved Successfully");
        }
    }
}
