using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadSceneAsync("PlayerWorld");
    }

    public void LoadContinueGame()
    {
        GameManager.instance.isNewGame = false;
        SceneManager.LoadSceneAsync("PlayerWorld");
    }

    public void LoadSettingUI()
    {
        UIManager.instance.SetActiveSettingUI(true);
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene("Credit");
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
        GameManager.instance.keySetting.CheckKeyOverlap();
        UIManager.instance.SetActiveSettingUI(false);
        if(SceneManager.GetActiveScene().name == "PlayerWorld")
        {
            WorldController.Instance.ExitSetting();
        }
    }

}
