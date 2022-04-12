using Script.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool canInterat;

    private void Start()
    {
        canInterat = false;
    }

    private void Update()
    {
        if (!WorldController.Instance.getIsPause())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                canInterat = true;
            if (Input.GetKeyUp(KeyCode.UpArrow))
                canInterat = false;
        }
        else
            canInterat = false;
    }

    //������ �浹 ���� �Լ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        IItem item = other.GetComponent<IItem>();
        if (item != null)
        {
            item.Use(gameObject);
        }
    }

    //��ȣ�ۿ� Ʈ���� �Լ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canInterat)
        {
            IInteraction interaction = collision.GetComponent<IInteraction>();
            if (interaction != null)
            {
                interaction.Interact(gameObject);
                canInterat = false;
            }
        }
    }
}
