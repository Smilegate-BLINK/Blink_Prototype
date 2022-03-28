using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    // 월드 싱글톤화
    private static WorldController _instance;
    public static WorldController Instance { get { return _instance; } }


    private GameObject myPlayer;
    private PlayerBlink plBlink;

    [Header("눈 감기 매커니즘 실행 여부")]
    public bool doBlinkFunc = false;
    [SerializeField, Header("맵의 발판들이 흐려지기 시작하는 시간")]
    private float startshadedTime = 1f;
    [SerializeField, Header("맵의 발판들이 완전히 흐려지는데 걸리는 시간")]
    private float shadedTimeTaken = 3f;
    private float time;


    private bool isPause;
    // true시 암전
    private bool worldBlackOut;
    // 1이 불투명, 0이 투명
    private float worldAlpha;


    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        myPlayer = GameObject.Find("Player");
        plBlink = myPlayer.GetComponent<PlayerBlink>();
        worldBlackOut = !plBlink.getEyeOpend();
        worldAlpha = 1f;
        isPause = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // 환경설정 진입, 탈출
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
            EnterSetting();
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
            ExitSetting();

        // 눈 깜빡임 관련
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
        // 눈이 감겨있으면
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
    
    private void EnterSetting()
    {
        isPause = true;
        Time.timeScale = 0f;
        // 설정창 띄우기
    }

    private void ExitSetting()
    {
        //설정창 닫기
        Time.timeScale = 1f;
        isPause = false;
    }
}
