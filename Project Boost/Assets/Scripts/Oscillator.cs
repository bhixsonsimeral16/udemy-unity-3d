using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycleState = Time.time % period;    // Value resets every period
        
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycleState * tau);

        movementFactor = (rawSinWave + 1f) / 2f;  // Change sin wave to go from 0 - 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
