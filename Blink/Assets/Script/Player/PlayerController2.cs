using Script.Item;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Animator myAnim;
    private BoxCollider2D myCol;
    private Rigidbody2D myRigid;
    private GroundCheck myGround;
    private WallCheck myWall;
    private PlayerBlink myBlink;

    [HideInInspector]
    public PhysicsMaterial2D groundPM;
    [HideInInspector]
    public PhysicsMaterial2D slipperPM;

    [Header("�̵��ӵ�")]
    public float speed = 5f;
    [Header("������")]
    public float jumpForce = 12f;
    [Header("��ư ������ ���� ������ ����")]
    public float jumpDiff = 0.6f;

    private int horizontal = 0;
    [HideInInspector]
    public int tempSaveSpot = 0;

    private KeySetting keySetting;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myCol = GetComponent<BoxCollider2D>();
        myRigid = GetComponent<Rigidbody2D>();
        myGround = transform.GetChild(0).GetComponent<GroundCheck>();
        myWall = transform.GetChild(1).GetComponent<WallCheck>();
<<<<<<< HEAD
        myBlink = GetComponent<PlayerBlink>();
=======
        keySetting = FindObjectOfType<KeySetting>();
>>>>>>> b5a1f89338fec326b649848c78c55be6106e9b84

        if (!GameManager.instance.isNewGame)
        {
            string fName = string.Format(Application.streamingAssetsPath + "/DataFiles", "PlayerInfo");
            if (File.Exists(fName))
            {
                PlayerInfo info = GameManager.instance.fileIOHelper.LoadJsonFile<PlayerInfo>(Application.streamingAssetsPath + "/DataFiles", "PlayerInfo");
                WorldController.Instance.saveSpot = info.saveSpot;
                MovetoSpot(WorldController.Instance.savePoints[info.saveSpot].transform.position);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (WorldController.Instance.getIsPause() == false)
        {
            GetHorizontalDirection();
            ChangeFriction();
            Move();
            PlayerAnimation();
        }
    }

    // �¿� �Է¿� ���� ���� �޾ƿ��� �Լ�
    private void GetHorizontalDirection()
    {
        if (Input.GetKey(keySetting.UserKey[KeyAction.LEFT]) && Input.GetKey(keySetting.UserKey[KeyAction.RIGHT]))
            horizontal = 0;
        else if (Input.GetKey(keySetting.UserKey[KeyAction.LEFT]))
            horizontal = -1;
        else if (Input.GetKey(keySetting.UserKey[KeyAction.RIGHT]))
            horizontal = 1;
        else
            horizontal = 0;
    }

    // ���� ��� �ִ� ������ ������ ���� �������� �ٲ�
    private void ChangeFriction()
    {
        if (myGround.isGrounded || !myGround.canJump)
            myCol.sharedMaterial = groundPM;
        else
            myCol.sharedMaterial = slipperPM;
    }

    // ������ ���� �Լ�
    private void Move()
    {
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
        if (myGround.canJump && Input.GetKeyDown(keySetting.UserKey[KeyAction.JUMP]))
            myRigid.AddForce(new Vector2(myRigid.velocity.x, jumpForce), ForceMode2D.Impulse);

        if (Input.GetKeyUp(keySetting.UserKey[KeyAction.JUMP]) && myRigid.velocity.y > 0f)
            myRigid.velocity = (new Vector2(myRigid.velocity.x, myRigid.velocity.y * jumpDiff));
    }

    // �� ������Ʈ�� ���� ����ְ�, ������ �� ������ ���� �ݴ� �������� �������� ������
    private void DoWallJump(float horizontal)
    {
        if (myWall.hitWall && !myGround.canMove)
        {
            if (myWall.hitRightWall && horizontal < 0 && Input.GetKeyDown(keySetting.UserKey[KeyAction.JUMP]))
                myRigid.AddForce(new Vector2(-speed, jumpForce), ForceMode2D.Impulse);
            else if (myWall.hitLeftWall && horizontal > 0 && Input.GetKeyDown(keySetting.UserKey[KeyAction.JUMP]))
                myRigid.AddForce(new Vector2(speed, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void PlayerAnimation()
    {
        if (myRigid.velocity.x < -0.1f && transform.localScale.x > 0f)
            transform.localScale = transform.localScale - new Vector3(transform.localScale.x * 2, 0f, 0f);
        if (myRigid.velocity.x > 0.1f && transform.localScale.x < 0f)
            transform.localScale = transform.localScale - new Vector3(transform.localScale.x * 2, 0f, 0f);

        if (myGround.canMove && horizontal == 0)
            myAnim.SetBool("isWalking", false);
        if (myGround.canMove && horizontal != 0)
            myAnim.SetBool("isWalking", true);

        if (myGround.isGrounded)
            myAnim.SetBool("isJumping", false);
        if (!myGround.isGrounded)
            myAnim.SetBool("isJumping", true);
        if (!myGround.isGrounded && !myWall.hitWall)
        {
            myAnim.SetBool("isHolding", false);
            myBlink.holding = false;
        }
        if (!myGround.isGrounded && myWall.hitWall)
        {
            myAnim.SetBool("isHolding", true);
            myBlink.holding = true;
        }
            
    }

    // �÷��̾ ���� �̵�(�ǹ� ���� �̵� ��)�� ���ϴ� ��Ȳ�� �� ȣ��Ǵ� �Լ�.
    public void MovetoSpot(Vector2 pos)
    {
        // ȭ�� ���̵�ƿ�
        transform.position = pos;
        myRigid.velocity = Vector2.zero;
        CameraController.Instance.SetCameraPos();
        // ȭ�� ���̵���
    }

    private void OnApplicationQuit()
    {
        PlayerInfo info = new PlayerInfo(this);
        var jsonData = JsonUtility.ToJson(info);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.streamingAssetsPath + "/DataFiles", "PlayerInfo", jsonData);
    }
}