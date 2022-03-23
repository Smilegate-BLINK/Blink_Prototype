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
            text[i].text = KeyManager.instance.userKey[(KeyAction)i].ToString();
        }
    }

    private void Init()
    {
        for (int i = 0; i < text.Length; ++i)
        {
            text[i].text = KeyManager.instance.userKey[(KeyAction)i].ToString();
        }
    }

   public void SetActiveSettingUI(bool isActive)
    {
        SettingUI.SetActive(isActive);
    }
}
