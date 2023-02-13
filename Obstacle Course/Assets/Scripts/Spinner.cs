using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xSpin = 0f;
    [SerializeField] float ySpin = 0f;
    [SerializeField] float zSpin = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xSpin, ySpin, zSpin);
    }
}
