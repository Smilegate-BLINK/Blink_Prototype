using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGroundController : MonoBehaviour
{
    [SerializeField]
    float fallSec = 0.5f;
    [SerializeField]
    float destroySec = 3f;

    private bool isTriggerd;

    Rigidbody2D myRigid;
    BoxCollider2D myCol;
    // Start is called before the first frame update
    void Start()
    {
        isTriggerd = false;
        myRigid = GetComponent<Rigidbody2D>();
        myCol = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            float thisHeight = (transform.localScale.y + collision.transform.localScale.y) * 0.5f + transform.position.y;
            if (collision.transform.position.y >= thisHeight)
            {
                isTriggerd = true;
                Invoke("FallPlatform", fallSec);
                Destroy(gameObject, destroySec);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && isTriggerd)
        {
            myCol.enabled = false;
        }
    }

    private void FallPlatform()
    {
        myRigid.isKinematic = false;
    }
}
