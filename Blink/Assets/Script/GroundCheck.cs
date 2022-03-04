using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool checkBounceOption;

    public bool isGrounded;
    public bool isSlided;

    public void SetBoundOption(bool option)
    {
        checkBounceOption = option;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGrounded && collision.tag == "Ground" && !checkBounceOption)
            isGrounded = true;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
            isGrounded = false;
    }
}
