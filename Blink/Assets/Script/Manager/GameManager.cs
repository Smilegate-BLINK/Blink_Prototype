using Script.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("시작 세이브 지점")]
    public GameObject savePoint;

    private KeyManager _keySetting;
    public KeyManager keySetting
    {
        get => _keySetting;
        private set => _keySetting = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            _keySetting = new KeyManager();
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

    public void SetSavepoint(GameObject elevator)
    {
        this.savePoint = elevator;
    }

    public void MoveToSavepoint(GameObject player)
    {
        player.transform.position = savePoint.transform.position;
    }
}
