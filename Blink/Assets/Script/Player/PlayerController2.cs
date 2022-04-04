using Script.Item;
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

    private int horizontal = 0;

    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<BoxCollider2D>();
        myRigid = GetComponent<Rigidbody2D>();
        myGround = transform.GetChild(0).GetComponent<GroundCheck>();
        myWall = transform.GetChild(1).GetComponent<WallCheck>();

        if (!GameManager.instance.isNewGame)
        {
            PlayerInfo info = GameManager.instance.fileIOHelper.LoadJsonFile<PlayerInfo>(Application.dataPath + "/DataFiles", "PlayerInfo");
            gameObject.transform.position = info.position;
            gameObject.transform.rotation = info.rotation;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        GetHorizontalDirection();
        ChangeFriction();
        Move();
    }

    private void GetHorizontalDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            horizontal = 0;
        else if (Input.GetKey(KeyCode.LeftArrow))
            horizontal = -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            horizontal = 1;
        else
            horizontal = 0;
    }

    // 현재 밟고 있는 발판의 종류에 따라 마찰력이 바뀜
    private void ChangeFriction()
    {
        if (myGround.isGrounded || !myGround.canJump)
            myCol.sharedMaterial = groundPM;
        else
            myCol.sharedMaterial = slipperPM;
    }

    private void Move()
    {
        DoWallJump(horizontal);
        // 이 오브젝트가 움직일 수 있거나, 움직일 수 없지만 y축으로 움직이지 않으면 좌우 움직임 가능
        if (myGround.canMove || (!myGround.canMove && myRigid.velocity.y == 0f && !myWall.hitWall))
        {
            myRigid.AddForce(new Vector2(horizontal * speed, 0f));
            if (myRigid.velocity.x > speed)
                myRigid.velocity = new Vector2(speed, myRigid.velocity.y);
            else if (myRigid.velocity.x < -speed)
                myRigid.velocity = new Vector2(-speed, myRigid.velocity.y);
        }

        // 점프 및 점프키 누름 정도에 따라 점프력 결정
        if (myGround.canJump && Input.GetKeyDown(KeyCode.Space))
            myRigid.AddForce(new Vector2(myRigid.velocity.x, jumpForce), ForceMode2D.Impulse);

        if (Input.GetKeyUp(KeyCode.Space) && myRigid.velocity.y > 0f)
            myRigid.velocity = (new Vector2(myRigid.velocity.x, myRigid.velocity.y * jumpDiff));
    }

    // 이 오브젝트가 벽에 닿아있고, 움직일 수 없으면 벽의 반대 방향으로 벽점프가 가능함
    private void DoWallJump(float horizontal)
    {
        if (myWall.hitWall && !myGround.canMove)
        {
            if (myWall.hitRightWall && horizontal < 0 && Input.GetButtonDown("Jump"))
                myRigid.AddForce(new Vector2(-speed, jumpForce), ForceMode2D.Impulse);
            else if (myWall.hitLeftWall && horizontal > 0 && Input.GetButtonDown("Jump"))
                myRigid.AddForce(new Vector2(speed, jumpForce), ForceMode2D.Impulse);
        }
    }
}