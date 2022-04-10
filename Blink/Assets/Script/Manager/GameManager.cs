using Script.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public FileIOHelper fileIOHelper;
    public bool isNewGame;
    [HideInInspector]
    public KeySetting keySetting;
    [HideInInspector]
    public VolumeSetting volumeSetting;
    [HideInInspector]
    public ResolutionSetting resolutionSetting;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            Init();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        fileIOHelper = new FileIOHelper();
        isNewGame = true;

        //세팅 값 불러오기
        keySetting = FindObjectOfType<KeySetting>();
        volumeSetting = FindObjectOfType<VolumeSetting>();
        resolutionSetting = FindObjectOfType<ResolutionSetting>();
    }

    private void OnApplicationQuit()
    {
    }
}
