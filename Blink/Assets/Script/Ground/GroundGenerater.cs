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
        // 리셋당하면 현재 오브젝트 삭제하고 다시 생성
    }
}
