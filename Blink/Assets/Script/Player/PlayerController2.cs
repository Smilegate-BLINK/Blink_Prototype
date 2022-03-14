using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private BoxCollider2D myCol;
    private Rigidbody2D myRigid;
    private GroundCheck myGround;
    private WallCheck myWall;

    [HideInInspector]
    public PhysicsMaterial2D groundPM;
    [HideInInspector]
    public PhysicsMaterial2D slipperPM;

    [Header("이동속도")]
    public float speed = 4f;
    [Header("점프력")]
    public float jumpForce = 12f;
    [Header("버튼 누름에 따른 점프력 차이")]
    public float jumpDiff = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<BoxCollider2D>();
        myRigid = GetComponent<Rigidbody2D>();
        myGround = transform.GetChild(0).GetComponent<GroundCheck>();
        myWall = transform.GetChild(1).GetComponent<WallCheck>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        ChangeFriction();
    }

    private void ChangeFriction()
    {
        if (myGround.isSlippered)
            myCol.sharedMaterial = slipperPM;
        else
            myCol.sharedMaterial = groundPM;
    }

    private void Move()
    {
        int horizontal;
        horizontal = (int)Input.GetAxis("Horizontal");

        DoWallJump(horizontal);
        if (myGround.isGrounded && Input.GetButtonDown("Jump"))
        {
            myRigid.AddForce(new Vector2(myRigid.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetButtonUp("Jump") && myRigid.velocity.y > 0.0f)
            myRigid.velocity = (new Vector2(myRigid.velocity.x, myRigid.velocity.y * jumpDiff));
        if (myGround.isGrounded && !myGround.isSloped)
        {
            myRigid.AddForce(new Vector2(horizontal * speed, 0f));
            if (myRigid.velocity.x > speed)
                myRigid.velocity = new Vector2(speed, myRigid.velocity.y);
            else if (myRigid.velocity.x < -speed)
                myRigid.velocity = new Vector2(-speed, myRigid.velocity.y);
        }
    }

    private void DoWallJump(float horizontal)
    {
        if (myWall.hitWall && !myGround.isGrounded)
        {
            if (myWall.hitRightWall && horizontal < 0 && Input.GetButtonDown("Jump"))
                myRigid.AddForce(new Vector2(-speed, jumpForce), ForceMode2D.Impulse);
            else if (myWall.hitLeftWall && horizontal > 0 && Input.GetButtonDown("Jump"))
                myRigid.AddForce(new Vector2(speed, jumpForce), ForceMode2D.Impulse);
        }
    }
}