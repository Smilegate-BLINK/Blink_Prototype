using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    private GameObject myPlayer;
    private PlayerBlink plBlink;

    // true시 암전
    private bool worldBlackOut;

    [SerializeField, Header("맵의 발판들이 흐려지기 시작하는 시간")]
    private float startshadedTime = 1f;
    [SerializeField, Header("맵의 발판들이 완전히 흐려지는데 걸리는 시간")]
    private float shadedTimeTaken = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        myPlayer = GameObject.Find("Player");
        plBlink = myPlayer.GetComponent<PlayerBlink>();
        worldBlackOut = !plBlink.getEyeOpend();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        worldBlackOut = !plBlink.getEyeOpend();
    }

    public bool getWorldBlackOut()
    {
        return worldBlackOut;
    }

    public float getStartshadedTime()
    {
        return startshadedTime;
    }

    public float getShadedTimeTaken()
    {
        return shadedTimeTaken;
    }
}
