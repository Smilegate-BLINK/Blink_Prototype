using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    private GameObject SettingUI;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        var Canvas = GameObject.Find("Canvas");
        SettingUI = Canvas.transform.Find("SettingUI").gameObject;
        Screen.SetResolution(1920, 1080, true);
    }

   public void SetActiveSettingUI(bool isActive)
    {
        SettingUI.SetActive(isActive);
    }

    public void ChangeScreenResolution(int isWindow)
    {
        if(isWindow == 1)
        {
            Screen.SetResolution(1080, 720, false);
        }
        else
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }
}
