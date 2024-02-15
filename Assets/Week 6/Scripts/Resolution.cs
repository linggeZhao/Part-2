using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resolution1()
    {
        Screen.SetResolution(1600, 900, false);
    }

    public void resolution2()
    {
        Screen.SetResolution(1920, 1080, false);
    }
}
