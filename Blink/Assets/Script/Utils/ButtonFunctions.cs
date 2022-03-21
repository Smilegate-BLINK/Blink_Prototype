using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void LoadContinueGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void LoadSettingUI()
    {
        
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
}
