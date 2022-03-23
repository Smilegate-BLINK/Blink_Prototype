using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    private GameObject myPlayer;
    private PlayerBlink plBlink;

    [Header("�� ���� ��Ŀ���� ���� ����")]
    public bool doBlinkFunc = false;
    [SerializeField, Header("���� ���ǵ��� ������� �����ϴ� �ð�")]
    private float startshadedTime = 1f;
    [SerializeField, Header("���� ���ǵ��� ������ ������µ� �ɸ��� �ð�")]
    private float shadedTimeTaken = 3f;
    private float time;

    // true�� ����
    private bool worldBlackOut;
    // 1�� ������, 0�� ����
    private float worldAlpha;

    // Start is called before the first frame update
    private void Start()
    {
        myPlayer = GameObject.Find("Player");
        plBlink = myPlayer.GetComponent<PlayerBlink>();
        worldBlackOut = !plBlink.getEyeOpend();
        worldAlpha = 1f;
    }

    // Update is called once per frame
    private void Update()
    {
        // �ӽ� ���� ���� �ڵ�
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


        // �� ������ ����
        worldBlackOut = !plBlink.getEyeOpend();
        if (doBlinkFunc)
            changeWorldAlpha();
        else
        {
            worldBlackOut = false;
            worldAlpha = 1f;
        }
            
    }

    private void changeWorldAlpha()
    {
        // ���� ����������

        if (worldBlackOut)
        {
            time += Time.deltaTime;
            if (time > shadedTimeTaken + startshadedTime)
                worldAlpha = 0f;
            else if (time > startshadedTime)
                worldAlpha = Mathf.Lerp(1f, 0f, (time - startshadedTime) / shadedTimeTaken);
        }
        else
        {
            time = 0f;
            worldAlpha = 1f;
        }  
    }

    public bool getWorldBlackOut()
    {
        return worldBlackOut;
    }

    public float getWorldAlpha()
    {
        return worldAlpha;
    }

    public void IncreaseShadeTime(float fDeltaTime)
    {
        shadedTimeTaken += fDeltaTime;
    }
}
