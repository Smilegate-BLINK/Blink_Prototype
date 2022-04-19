using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour, IInteraction
{
    public void Interact(GameObject target)
    {
        SceneManager.LoadScene("EndingScene");
    }
}
