using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteraction
{
    public virtual void Interact(GameObject target)
    {
        if (target.tag == "Player")
        {
            PlayerController2 myPlayer = target.GetComponent<PlayerController2>();
            //GameManager.instance.SetSavepoint(gameObject);
            myPlayer.MovetoSpot(WorldController.Instance.savePoints[WorldController.Instance.saveSpot].transform.position);
        }
    }
}
