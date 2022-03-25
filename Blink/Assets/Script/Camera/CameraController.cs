using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private WorldController myWorld;

    private Vector3 camPos;
    private Vector2 playerPos;
    private float xScreenHalfSize;
    private float yScreenHalfSize;
    private float camearDepth;

    private Transform myPlayer;
    private Camera myCamera;

    private void Awake()
    {
        myWorld = GameObject.Find("World").GetComponent<WorldController>();
        myCamera = GetComponent<Camera>();
        myPlayer = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        yScreenHalfSize = myCamera.orthographicSize;
        xScreenHalfSize = myCamera.aspect * yScreenHalfSize;
        camearDepth = myCamera.transform.position.z;
        myCamera.transform.position = myPlayer.transform.position + new Vector3(0, 0, camearDepth);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        moveCamera();
        changeBackGround();
    }

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
        if (myWorld.getWorldBlackOut())
            myCamera.backgroundColor = Color.black;
        else
            myCamera.backgroundColor = Color.white;
    }
}
