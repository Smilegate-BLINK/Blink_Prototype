using Script.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            IInteraction interaction = collision.GetComponent<IInteraction>();
            if (interaction != null)
            {
                interaction.Interact(gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        //PlayerInfo playerInfo = new PlayerInfo(this);
        //playerInfo.position = gameObject.transform.position;
        //playerInfo.rotation = gameObject.transform.rotation;
        //string data = playerInfo.ObjectToJson();
        //GameManager.instance.fileIOHelper.CreateJsonFile(Application.dataPath + "/DataFiles", "PlayerInfo", data);
    }
}
