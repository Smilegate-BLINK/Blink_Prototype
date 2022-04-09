using Script.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("���� ��ȣ�ۿ���� �ɸ��� �ð�")]
    public float deltaInteract;
    private float interactTime;
    private void Start()
    {
        interactTime = 0f;
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
        if (Input.GetKey(KeyCode.UpArrow) && interactTime + deltaInteract < Time.realtimeSinceStartup)
        {
            IInteraction interaction = collision.GetComponent<IInteraction>();
            if (interaction != null)
            {
                interactTime = Time.realtimeSinceStartup;
                interaction.Interact(gameObject);
            }
        }
    }
}
