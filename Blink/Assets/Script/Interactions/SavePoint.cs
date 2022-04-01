using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private bool isUsable;
    private int spotNumber;

    // Start is called before the first frame update
    void Start()
    {
        isUsable = false;
        spotNumber = WorldController.Instance.savePoints.IndexOf(this);
    }

    public void Interact(GameObject target)
    {
        if (!isUsable)
        {
            isUsable = true;
            //GameManager.instance.SetSavepoint(gameObject);
            WorldController.Instance.saveSpot = spotNumber;
            print("새로운 세이브 포인트를 지정했습니다.");
        }
    }
}
