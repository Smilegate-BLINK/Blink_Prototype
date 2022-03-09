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
    public float jumpForce = 24;
    [Header("버튼 누름에 따른 점프력 차이")]
    public float jumpDiff = 0.6f;

    private float fixedHor, savedHor;

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
        //Blink();
    }

    private void Move()
    {
        float horizontal;

        horizontal = Input.GetAxis("Horizontal");
        GetFixedHor(horizontal);
        if (myGround.isGrounded && Input.GetButtonDown("Jump"))
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
            savedHor = horizontal;
        }
        if (Input.GetButtonUp("Jump") && rigidbody2d.velocity.y > 0.0f)
            rigidbody2d.velocity = (new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y * jumpDiff));
        rigidbody2d.velocity = new Vector2(fixedHor * speed, rigidbody2d.velocity.y);
    }

    private void GetFixedHor(float horizontal)
    {
        fixedHor = horizontal;
        if (myGround.isGrounded)
            savedHor = horizontal;
        if (!myGround.isGrounded)
            fixedHor = savedHor;
        if (myWall.hitLeftWall && horizontal < 0f)
            fixedHor = 0f;
        if (myWall.hitRightWall && horizontal > 0f)
            fixedHor = 0f;
        if ((myWall.hitLeftWall || myWall.hitRightWall) && !myGround.isGrounded)
            fixedHor = 0;
    }
}