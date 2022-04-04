using Script.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("시작 세이브 지점")]
    public GameObject savePoint;

    public FileIOHelper fileIOHelper;
    public bool isNewGame;

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
    }

    public void SetSavepoint(GameObject elevator)
    {
        this.savePoint = elevator;
    }

    public void MoveToSavepoint(GameObject player)
    {
        player.transform.position = savePoint.transform.position;
    }

    private void OnApplicationQuit()
    {
    }
}
