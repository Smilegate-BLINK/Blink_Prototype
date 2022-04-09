using Script.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("다음 상호작용까지 걸리는 시간")]
    public float deltaInteract;
    private float interactTime;
    private void Start()
    {
        interactTime = 0f;
    }
    //아이템 충돌 실행 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        IItem item = other.GetComponent<IItem>();
        if (item != null)
        {
            item.Use(gameObject);
        }
    }

    //상호작용 트리거 함수
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
