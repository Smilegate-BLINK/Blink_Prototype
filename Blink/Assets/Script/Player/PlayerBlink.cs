using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [Header("눈을 뜰 수 있는 시간")]
    public float eyeOpenTime = 5f;
    [Header("강제로 감겼을 때 다시 뜰 수 있을때까지 걸리는 시간")]
    public float forceClosedTimer = 10f;

    private bool eyeOpend;
    private bool forcedClose;
    private float eyetime;  // 눈을 열고닫는 타이머
    private float fctime;  // 강제로 눈을 감게 하는 시간 타이머

    private SpriteRenderer eyeSprite;
    public Sprite eyeOpenSprite;
    public Sprite eyeClosedSprite;

    private KeySetting keySetting;

    // Start is called before the first frame update
    private void Start()
    {
        eyeOpend = false;
        forcedClose = false;
        eyetime = 0f;
        fctime = forceClosedTimer;
        eyeSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
        keySetting = FindObjectOfType<KeySetting>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!WorldController.Instance.getIsPause())
        {
            if (Input.GetKeyDown(keySetting.UserKey[KeyAction.BLINK]))
                eyeOpend = !eyeOpend;
        }
        if (eyeOpend)
            eyeSprite.sprite = eyeOpenSprite;
        else
            eyeSprite.sprite = eyeClosedSprite;
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

        if (!WorldController.Instance.doBlinkFunc)
        {
            eyetime = 0f;
            fctime = 0f;
        }
    }

    public bool getEyeOpend()
    {
        return eyeOpend;
    }
}
