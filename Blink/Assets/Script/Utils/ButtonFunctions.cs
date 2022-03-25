using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadSceneAsync("PlayerWorld 1");
    }

    public void LoadContinueGame()
    {
        GameManager.instance.isNewGame = false;
        SceneManager.LoadSceneAsync("PlayerWorld 1");
    }

    public void LoadSettingUI()
    {
        UIManager.instance.SetActiveSettingUI(true);
    }

    public void LoadCredit()
    {

    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void QuitSetting()
    {
        UIManager.instance.SetActiveSettingUI(false);
        KeyManager.instance.CheckKeyOverlap();
    }

}
