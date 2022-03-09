using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    private GameObject myPlayer;
    private PlayerBlink plBlink;

    // true�� ����
    private bool worldBlackOut;

    [SerializeField, Header("���� ���ǵ��� ������� �����ϴ� �ð�")]
    private float startshadedTime = 1f;
    [SerializeField, Header("���� ���ǵ��� ������ ������µ� �ɸ��� �ð�")]
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
