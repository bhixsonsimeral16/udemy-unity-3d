using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Vector3 playerInput;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        PrintInstructions();
    }

    // Update is called once per frame
    void Update()
    {
        GetUserInput();
    }

    private void FixedUpdate()
    {
        rb.AddForce(playerInput * moveSpeed);
    }

    private void PrintInstructions()
    {
        Debug.Log("Welcome to the shit, recruit!");
        Debug.Log("Move with WASD or arrow keys.");
        Debug.Log("Don't hit stuff.");
    }

    private void GetUserInput()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.z = Input.GetAxis("Vertical");
        playerInput = Vector3.ClampMagnitude(playerInput, 1f);
    }
}
