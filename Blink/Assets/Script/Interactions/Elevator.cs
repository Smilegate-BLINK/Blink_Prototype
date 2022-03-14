using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Item;

public class Elevator : MonoBehaviour, IInteraction
{
    private bool isUsable;
    // Start is called before the first frame update
    void Start()
    {
        isUsable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact(GameObject target)
    {
        if(!isUsable)
        {
            isUsable = true;
            GameManager.instance.SetSavepoint(gameObject);
            print("새롭게 세이브존이 생성되었습니다");
        }
        else
        {
            GameManager.instance.MoveToSavepoint(target);
            print("세이브존으로 이동합니다");
        }
    }
}
