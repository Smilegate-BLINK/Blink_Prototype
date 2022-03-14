using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool checkBounceOption;

    public bool isGrounded;
    public bool isSlippered;
    public bool isSloped;

    public void SetBoundOption(bool option)
    {
        checkBounceOption = option;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGrounded && collision.tag == "Ground" && !checkBounceOption)
            isGrounded = true;
        else if (!isGrounded && collision.tag == "Slope")
        {
            isGrounded = true;
            isSloped = true;
        }
        else if (!isGrounded && collision.tag == "Slipper")
        {
            isGrounded = true;
            isSlippered = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isGrounded && collision.tag == "Ground" && !checkBounceOption)
            isGrounded = true;
        else if (!isGrounded && collision.tag == "Slope")
        {
            isGrounded = true;
            isSloped = true;
        }
        else if (!isGrounded && collision.tag == "Slipper")
        {
            isGrounded = true;
            isSlippered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
            isGrounded = false;
        else if (collision.tag == "Slope")
        {
            isGrounded = false;
            isSloped = false;
        }
        else if (collision.tag == "Slipper")
        {
            isGrounded = false;
            isSlippered = false;
        }
    }
}
