using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField]
    private GameObject SettingUI;
    [SerializeField]
    private Text[] text;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
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

    private void Update()
    {
        for (int i = 0; i < text.Length; ++i)
        {
            //text[i].text = KeyManager.instance.userKey[(KeyAction)i].ToString();
        }
    }

    private void Init()
    {
        SettingUI = GameObject.Find("SettingUI");
        Screen.SetResolution(1920, 1080, true);
        for (int i = 0; i < text.Length; ++i)
        {
            //text[i].text = KeyManager.instance.userKey[(KeyAction)i].ToString();
        }
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
