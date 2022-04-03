using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpGroundController : MonoBehaviour
{
    private Transform thisTransform;
    public float jumpHeight = 16f;

    private float rotationZ;
    private float forceToX;
    private float forceToY;
    
    private void Start()
    {
        thisTransform = GetComponent<Transform>();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        rotationZ = thisTransform.rotation.eulerAngles.z;
        forceToX = Mathf.Sin(rotationZ) * jumpHeight;
        forceToY = Mathf.Cos(rotationZ) * jumpHeight;
        if (collision.transform.tag == "Player")
        {
            Rigidbody2D colRigid = collision.GetComponent<Rigidbody2D>();
            colRigid.AddForce(new Vector2(colRigid.velocity.x + forceToX, forceToY), ForceMode2D.Impulse);
        }
            
    }
}
