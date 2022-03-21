using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGroundController : MonoBehaviour
{
    [SerializeField]
    float fallSec = 0.5f;
    [SerializeField]
    float destroySec = 3f;

    Rigidbody2D myRigid;
    BoxCollider2D myCol;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myCol = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Invoke("FallPlatform", fallSec);
            Destroy(gameObject, destroySec);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            myCol.enabled = false;
        }
    }

    private void FallPlatform()
    {
        myRigid.isKinematic = false;
    }
}
