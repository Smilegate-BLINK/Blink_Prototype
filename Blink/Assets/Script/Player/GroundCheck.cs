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
        canJump = isGrounded || isSloped || isSlippered;

        if (isGrounded && isSloped)
            isSloped = false;
        if (isGrounded && isSlippered)
            isSlippered = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isGrounded)
        {
            switch (collision.tag)
            {
                case "Ground":
                    isGrounded = true;
                    break;
                case "Slope":
                    isSloped = true;
                    break;
                case "Slipper":
                    isSlippered = true;
                    break;
                default:
                    Debug.LogError(collision.tag);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ground":
                isGrounded = false;
                break;
            case "Slope":
                isSloped = false;
                break;
            case "Slipper":
                isSlippered = false;
                break;
            default:
                break;
        }
    }
}
