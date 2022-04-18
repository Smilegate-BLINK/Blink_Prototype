using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라컨트롤러 싱글톤화
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }

    private Vector3 camPos;
    private Vector2 playerPos;
    private float xScreenHalfSize;
    private float yScreenHalfSize;
    private float camearDepth = -10f;

    private Transform myPlayer;
    private Camera myCamera;

    [Header("시작 시 카메라를 플레이어 기준에서 위치시킬 X좌표")]
    public float initialPosX;
    [Header("시작 시 카메라를 플레이어 기준에서 위치시킬 Y좌표")]
    public float initialPosY;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        myCamera = GetComponent<Camera>();
        myPlayer = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        yScreenHalfSize = myCamera.orthographicSize;
        xScreenHalfSize = myCamera.aspect * yScreenHalfSize;
        camearDepth = myCamera.transform.position.z;
        SetCameraPos();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        moveCamera();
        //changeBackGround();
    }

    // 플레이어가 순간이동할 때 시행되는 카메라 움직임
    public void SetCameraPos()
    {
        myCamera.transform.position = myPlayer.transform.position + new Vector3(initialPosX, initialPosY, camearDepth);
    }

    // 플레이어가 걸어 이동할 때 시행되는 카메라 움직임
    private void moveCamera()
    {
        
        playerPos = myPlayer.position;
        camPos = myCamera.transform.position;

        if (playerPos.x - camPos.x > xScreenHalfSize)
            camPos += new Vector3(xScreenHalfSize * 2, 0, camearDepth);
        else if (playerPos.x - camPos.x < -xScreenHalfSize)
            camPos += new Vector3(-xScreenHalfSize * 2, 0, camearDepth);
        else if (playerPos.y - camPos.y > yScreenHalfSize)
            camPos += new Vector3(0, yScreenHalfSize * 2, camearDepth);
        else if (playerPos.y - camPos.y < -yScreenHalfSize)
            camPos += new Vector3(0, -yScreenHalfSize * 2, camearDepth);
        myCamera.transform.position = camPos;
    }

    private void changeBackGround()
    { 
        if (WorldController.Instance.getWorldBlackOut())
            myCamera.backgroundColor = Color.black;
        else
            myCamera.backgroundColor = Color.white;
    }
}
