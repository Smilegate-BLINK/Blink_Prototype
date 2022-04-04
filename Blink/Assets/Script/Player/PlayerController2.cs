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

    [Header("�̵��ӵ�")]
    public float speed = 4f;
    [Header("������")]
    public float jumpForce = 12f;
    [Header("��ư ������ ���� ������ ����")]
    public float jumpDiff = 0.6f;

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
        ChangeFriction();
        Move();
    }

    // ���� ��� �ִ� ������ ������ ���� �װ��� �ٲ�
    private void ChangeFriction()
    {
        if (myGround.isGrounded || !myGround.canJump)
            myCol.sharedMaterial = groundPM;
        else
            myCol.sharedMaterial = slipperPM;
    }

    private void Move()
    {
        int horizontal;
        horizontal = (int)Input.GetAxis("Horizontal");

        DoWallJump(horizontal);
        // �� ������Ʈ�� ������ �� �ְų�, ������ �� ������ y������ �������� ������ �¿� ������ ����
        if (myGround.canMove || (!myGround.canMove && myRigid.velocity.y == 0f && !myWall.hitWall))
        {
            myRigid.AddForce(new Vector2(horizontal * speed, 0f));
            if (myRigid.velocity.x > speed)
                myRigid.velocity = new Vector2(speed, myRigid.velocity.y);
            else if (myRigid.velocity.x < -speed)
                myRigid.velocity = new Vector2(-speed, myRigid.velocity.y);
        }

        // ���� �� ����Ű ���� ������ ���� ������ ����
        if (myGround.canJump && Input.GetButtonDown("Jump"))
            myRigid.AddForce(new Vector2(myRigid.velocity.x, jumpForce), ForceMode2D.Impulse);

        if (Input.GetButtonUp("Jump") && myRigid.velocity.y > 0f)
            myRigid.velocity = (new Vector2(myRigid.velocity.x, myRigid.velocity.y * jumpDiff));
    }

    // �� ������Ʈ�� ���� ����ְ�, ������ �� ������ ���� �ݴ� �������� �������� ������
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