using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [Header("���� �� �� �ִ� �ð�")]
    public float eyeOpenTime = 5f;
    [Header("������ ������ �� �ٽ� �� �� ���������� �ɸ��� �ð�")]
    public float forceClosedTimer = 10f;

    [HideInInspector]
    public bool eyeOpend;
    private bool forcedClose;
    private float eyetime;  // ���� ����ݴ� Ÿ�̸�
    private float fctime;  // ������ ���� ���� �ϴ� �ð� Ÿ�̸�

    private SpriteRenderer eyeSprite;
    [HideInInspector]
    public Sprite eyeOpenSprite;
    [HideInInspector]
    public Sprite eyeClosedSprite;

    [HideInInspector]
    public bool holding;
    [HideInInspector]
    public bool sliding;
    [HideInInspector]
    public bool jumping;

    private Vector3 eyePos;
    private Vector3 eyeRevPos;

    private KeySetting keySetting;

    // Start is called before the first frame update
    private void Start()
    {
        eyeOpend = false;
        forcedClose = false;
        eyetime = 0f;
        fctime = forceClosedTimer;
        eyeSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();

        eyePos = eyeSprite.gameObject.transform.position - transform.position;
        eyeRevPos = new Vector3(-eyePos.x, eyePos.y, eyePos.z);
        holding = false;
        sliding = false;
        jumping = false;

        keySetting = FindObjectOfType<KeySetting>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!WorldController.Instance.getIsPause() && WorldController.Instance.playerCanMove)
        {
            if (Input.GetKeyDown(keySetting.UserKey[KeyAction.BLINK]))
                eyeOpend = !eyeOpend;
            eyeTimer();
        }
        eyeSpriteControl();
        
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

    private void eyeSpriteControl()
    {
        if (eyeOpend)
            eyeSprite.sprite = eyeOpenSprite;
        else
            eyeSprite.sprite = eyeClosedSprite;

        if (holding)
            eyeSprite.gameObject.transform.localScale = new Vector3(-1f, 1f, 0);
        else
            eyeSprite.gameObject.transform.localScale = new Vector3(1f, 1f, 0);

        if (transform.localScale.x > 0f)
        {
            if (holding)
                eyeSprite.gameObject.transform.position = transform.position + eyePos + new Vector3(-0.25f, 0.1f, 0f);
            else if (sliding)
                eyeSprite.gameObject.transform.position = transform.position + eyePos + new Vector3(0f, 0.2f, 0f);
            else if (jumping)
                eyeSprite.gameObject.transform.position = transform.position + eyePos + new Vector3(-0.05f, 0f, 0f);
            else
                eyeSprite.gameObject.transform.position = transform.position + eyePos;
        }
        else
        {
            if (holding)
                eyeSprite.gameObject.transform.position = transform.position + eyeRevPos + new Vector3(0.25f, 0.1f, 0f);
            else if (sliding)
                eyeSprite.gameObject.transform.position = transform.position + eyeRevPos + new Vector3(0f, 0.2f, 0f);
            else if (jumping)
                eyeSprite.gameObject.transform.position = transform.position + eyeRevPos + new Vector3(0.05f, 0f, 0f);
            else
                eyeSprite.gameObject.transform.position = transform.position + eyeRevPos;
        }
    }
}
