using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PrintToConsole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string testString = "This is a test of string format";
        string log = String.Format("{0}", testString);
        Debug.Log(log);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
