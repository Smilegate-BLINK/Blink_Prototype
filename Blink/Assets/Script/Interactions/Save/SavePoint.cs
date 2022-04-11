using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteraction
{
    public Transform insideBuilding;
    private int spotNumber;
    public bool isFixed;

    // Start is called before the first frame update
    void Start()
    {
        spotNumber = WorldController.Instance.savePoints.IndexOf(this);
        isFixed = false;
    }

    public virtual void Interact(GameObject target)
    {
        if (target.tag == "Player")
        {
            PlayerController2 myPlayer = target.GetComponent<PlayerController2>();
            //GameManager.instance.SetSavepoint(gameObject);
            if (myPlayer != null)
            {
                myPlayer.tempSaveSpot = spotNumber;
                myPlayer.MovetoSpot(insideBuilding.position);
                WorldController.Instance.doBlinkFunc = false;
            }
        }
    }
}
