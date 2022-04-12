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
    private float eyetime;  // ���� ����ݴ� Ÿ�̸�
    private float fctime;  // ������ ���� ���� �ϴ� �ð� Ÿ�̸�

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
