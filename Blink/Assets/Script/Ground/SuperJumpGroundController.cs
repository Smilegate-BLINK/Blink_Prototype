using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpGroundController : MonoBehaviour
{
    public float jumpHeight = 16f;

    private float rotationZ;
    private float forceToX;
    private float forceToY;

    private bool isUsed;

    private void Start()
    {
        isUsed = false;
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        rotationZ = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        forceToX = Mathf.Sin(rotationZ) * jumpHeight;
        forceToY = Mathf.Cos(rotationZ) * jumpHeight;
        Debug.Log(forceToX);
        if (collision.transform.tag == "Player" && !isUsed)
        {
            Rigidbody2D colRigid = collision.gameObject.GetComponent<Rigidbody2D>();
            colRigid.AddForce(new Vector2(colRigid.velocity.x + forceToX, forceToY), ForceMode2D.Impulse);
            isUsed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && isUsed)
            isUsed = false;        
    }
}
