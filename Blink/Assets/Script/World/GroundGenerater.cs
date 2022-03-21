using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerater : MonoBehaviour
{
    public GameObject fallGround;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(fallGround, this.transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
