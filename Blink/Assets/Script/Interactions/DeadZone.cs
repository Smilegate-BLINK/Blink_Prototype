using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour, IInteraction
{
    // Start is called before the first frame update
    public void Interact(GameObject target)
    {
        if (target.tag == "Player")
        {
            PlayerController2 myPlayer = target.GetComponent<PlayerController2>();
            //GameManager.instance.SetSavepoint(gameObject);
            myPlayer.MovetoSpot(WorldController.Instance.savePoints[WorldController.Instance.saveSpot].transform.position);
        }
    }
}
