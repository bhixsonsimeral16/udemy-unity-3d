using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody playerRigidbody;
    AudioSource audioSource;
    [SerializeField] float thrustForce = 100f;
    [SerializeField] float rotationForce = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        playerRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    void StopThrusting()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Stop();
        }
    }

    void RotateRight()
    {
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
        AddRotationalForce(-rotationForce);
    }

    void RotateLeft()
    {
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
        AddRotationalForce(rotationForce);
    }

    void StopRotating()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    void AddRotationalForce(float rotationThisFrame)
    {
        playerRigidbody.freezeRotation = true;  // freezing rotation so that we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        playerRigidbody.freezeRotation = false;
    }
}
