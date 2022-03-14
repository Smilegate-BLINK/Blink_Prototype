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
            print("���Ӱ� ���̺����� �����Ǿ����ϴ�");
        }
        else
        {
            GameManager.instance.MoveToSavepoint(target);
            print("���̺������� �̵��մϴ�");
        }
    }
}
