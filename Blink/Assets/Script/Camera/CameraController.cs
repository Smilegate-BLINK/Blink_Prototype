using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ī�޶���Ʈ�ѷ� �̱���ȭ
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }

    private Vector3 camPos;
    private Vector2 playerPos;
    private float xScreenHalfSize;
    private float yScreenHalfSize;
    private float camearDepth = -10f;

    private Transform myPlayer;
    private Camera myCamera;

    [Header("���� �� ī�޶� �÷��̾� ���ؿ��� ��ġ��ų X��ǥ")]
    public float initialPosX;
    [Header("���� �� ī�޶� �÷��̾� ���ؿ��� ��ġ��ų Y��ǥ")]
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
    }

    // �÷��̾ �����̵��� �� ����Ǵ� ī�޶� ������
    public void SetCameraPos()
    {
        myCamera.transform.position = myPlayer.transform.position + new Vector3(initialPosX, initialPosY, camearDepth);
    }

    // �÷��̾ �ɾ� �̵��� �� ����Ǵ� ī�޶� ������
    private void moveCamera()
    {
        
        playerPos = myPlayer.position;
        camPos = myCamera.transform.position;

        if (playerPos.x - camPos.x > xScreenHalfSize)
            camPos += new Vector3(xScreenHalfSize * 2, 0, camearDepth);
        if (playerPos.x - camPos.x < -xScreenHalfSize)
            camPos += new Vector3(-xScreenHalfSize * 2, 0, camearDepth);
        if (playerPos.y - camPos.y > yScreenHalfSize)
            camPos += new Vector3(0, yScreenHalfSize * 2, camearDepth);
        if (playerPos.y - camPos.y < -yScreenHalfSize)
            camPos += new Vector3(0, -yScreenHalfSize * 2, camearDepth);
        myCamera.transform.position = camPos;
    }
}
