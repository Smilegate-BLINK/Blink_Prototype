using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private GroundCheck myGround;
    private WallCheck myWall;

    [Header("이동속도")]
    public float speed = 6f;
    [Header("점프력")]
    public float jumpForce = 12f;
    [Header("버튼 누름에 따른 점프력 차이")]
    public float jumpDiff = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        myGround = transform.GetChild(0).GetComponent<GroundCheck>();
        myWall = transform.GetChild(1).GetComponent<WallCheck>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal;

        horizontal = Input.GetAxis("Horizontal");
        DoWallJump(horizontal);
        if (myGround.isGrounded && Input.GetButtonDown("Jump"))
        {
            rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetButtonUp("Jump") && rigidbody2d.velocity.y > 0.0f)
            rigidbody2d.velocity = (new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y * jumpDiff));
        if (myGround.isGrounded)
            rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);
    }

    private void DoWallJump(float horizontal)
    {
        if (myWall.hitWall && !myGround.isGrounded)
        {
            if (myWall.hitRightWall && horizontal < 0 && Input.GetButtonDown("Jump"))
                rigidbody2d.AddForce(new Vector2(-speed * 2 / 3, jumpForce), ForceMode2D.Impulse);
            else if (myWall.hitLeftWall && horizontal > 0 && Input.GetButtonDown("Jump"))
                rigidbody2d.AddForce(new Vector2(speed * 2 / 3, jumpForce), ForceMode2D.Impulse);
        }
    }
}