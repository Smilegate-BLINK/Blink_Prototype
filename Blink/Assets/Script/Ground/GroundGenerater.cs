using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerater : MonoBehaviour
{
    public GameObject fallGround;

    private GameObject myObject;
    // Start is called before the first frame update
    void Start()
    {
        myObject = Instantiate(fallGround, this.transform.position, Quaternion.identity);
    }

    void Update()
    {
        // ���´��ϸ� ���� ������Ʈ �����ϰ� �ٽ� ����
    }
}
