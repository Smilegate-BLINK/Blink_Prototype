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

    [Header("이동속도")]
    public float speed = 5f;
    [Header("점프력")]
    public float jumpForce = 12f;
    [Header("버튼 누름에 따른 점프력 차이")]
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

    // 좌우 입력에 따른 값을 받아오는 함수
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

    // 현재 밟고 있는 발판의 종류에 따라 마찰력이 바뀜
    private void ChangeFriction()
    {
        if (myGround.isGrounded || !myGround.canJump)
            myCol.sharedMaterial = groundPM;
        else
            myCol.sharedMaterial = slipperPM;
    }

    // 움직임 구현 함수
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
        if (myGround.canJump && Input.GetKeyDown(keySetting.UserKey[KeyAction.JUMP]))
            myRigid.AddForce(new Vector2(myRigid.velocity.x, jumpForce), ForceMode2D.Impulse);

        if (Input.GetKeyUp(keySetting.UserKey[KeyAction.JUMP]) && myRigid.velocity.y > 0f)
            myRigid.velocity = (new Vector2(myRigid.velocity.x, myRigid.velocity.y * jumpDiff));
    }

    // 이 오브젝트가 벽에 닿아있고, 움직일 수 없으면 벽의 반대 방향으로 벽점프가 가능함
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

    // 플레이어가 강제 이동(건물 내부 이동 등)을 당하는 상황일 때 호출되는 함수.
    public void MovetoSpot(Vector2 pos)
    {
        // 화면 페이드아웃
        transform.position = pos;
        myRigid.velocity = Vector2.zero;
        CameraController.Instance.SetCameraPos();
        // 화면 페이드인
    }

    private void OnApplicationQuit()
    {
        PlayerInfo info = new PlayerInfo(this);
        var jsonData = JsonUtility.ToJson(info);
        GameManager.instance.fileIOHelper.CreateJsonFile(Application.streamingAssetsPath + "/DataFiles", "PlayerInfo", jsonData);
    }
}