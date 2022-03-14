using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [Header("���� �� �� �ִ� �ð�")]
    public float eyeOpenTime = 5f;
    [Header("������ ������ �� �ٽ� �� �� ���������� �ɸ��� �ð�")]
    public float forceClosedTimer = 10f;

    private bool eyeOpend;
    private bool forcedClose;
    private float eyetime;
    private float fctime;
    
    // Start is called before the first frame update
    private void Start()
    {
        eyeOpend = false;
        forcedClose = false;
        eyetime = 0f;
        fctime = forceClosedTimer;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            eyeOpend = !eyeOpend;
        eyeTimer();
    }

    private void eyeTimer()
    {
        if (eyeOpend)
            eyetime += Time.deltaTime;
        else
            eyetime -= Time.deltaTime;
        if (eyetime < 0f)
            eyetime = 0f;
            
            
        if (eyetime > eyeOpenTime)
            forcedClose = true;
        if (forcedClose)
        {
            eyeOpend = false;
            eyetime = 0f;
            fctime -= Time.deltaTime;
        }
        if (fctime < 0f)
        {
            forcedClose = false;
            fctime = forceClosedTimer;
        }

    }

    public bool getEyeOpend()
    {
        return eyeOpend;
    }
}
