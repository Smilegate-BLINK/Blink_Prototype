using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteraction
{
    public Transform insideBuilding;
    private int spotNumber;

    // Start is called before the first frame update
    void Start()
    {
        spotNumber = WorldController.Instance.savePoints.IndexOf(this);
    }

    public virtual void Interact(GameObject target)
    {
        if (target.tag == "Player")
        {
            PlayerController2 myPlayer = target.GetComponent<PlayerController2>();
            //GameManager.instance.SetSavepoint(gameObject);
            myPlayer.tempSaveSpot = spotNumber;
            //print("새로운 세이브 포인트를 지정했습니다.");
            myPlayer.MovetoSpot(insideBuilding.position);
        }
    }
}
