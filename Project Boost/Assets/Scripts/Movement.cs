using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody playerRigidbody;
    [SerializeField] float thrustForce = 100f;
    [SerializeField] float rotationForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            AddRotationalForce(rotationForce);
        }

        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            AddRotationalForce(-rotationForce);
        }
    }

    void AddRotationalForce(float rotationThisFrame)
    {
        playerRigidbody.freezeRotation = true;  // freezing rotation so that we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        playerRigidbody.freezeRotation = false;
    }
}
