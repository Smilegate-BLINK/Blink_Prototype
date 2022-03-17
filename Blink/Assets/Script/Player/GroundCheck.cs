using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public bool canMove;
    public bool canJump;
    public bool isGrounded;
    public bool isSlippered;
    public bool isSloped;

    private void Update()
    {
        canMove = isGrounded || isSlippered;
        canJump = isGrounded || isSloped;

        if (isGrounded && isSloped)
            isSloped = false;
        if (isGrounded && isSlippered)
            isSlippered = false;
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGrounded && collision.tag == "Ground")
            isGrounded = true;
        else if (!isGrounded && collision.tag == "Slope")
            isSloped = true;
        else if (!isGrounded && collision.tag == "Slipper")
            isSlippered = true;
    }
*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isGrounded && collision.tag == "Ground")
            isGrounded = true; 
        else if (!isGrounded && collision.tag == "Slope")
            isSloped = true;
        else if (!isGrounded && collision.tag == "Slipper")
            isSlippered = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
            isGrounded = false;
        else if (collision.tag == "Slope")
            isSloped = false;
        else if (collision.tag == "Slipper")
            isSlippered = false;
    }
}
