using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerater : MonoBehaviour
{
    public GameObject Ground;

    private GameObject myObject;
    // Start is called before the first frame update
    void Start()
    {
        myObject = Instantiate(Ground, this.transform.position, Quaternion.identity);
    }

    void Update()
    {
        // ���´��ϸ� ���� ������Ʈ �����ϰ� �ٽ� ����
    }
}
