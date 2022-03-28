using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpGroundController : MonoBehaviour
{
    public float jumpHeight = 8f;

    private float checkHeight;

    private void OnCollisionStay2D(Collision2D collision)
    {
        checkHeight = (collision.transform.localScale.y + transform.localScale.y) / 2;
        if (collision.transform.tag == "Player" && collision.transform.position.y >= transform.position.y + checkHeight)
            collision.rigidbody.AddForce(new Vector2(collision.rigidbody.velocity.x, jumpHeight), ForceMode2D.Impulse);
    }
}
