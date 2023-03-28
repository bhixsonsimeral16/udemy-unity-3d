using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float xVelocity = 25f;
    [SerializeField] float yVelocity = 25f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;
    [SerializeField] float positionPitchFactor = -1f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float rotationFactor = 0.7f;

    InputAction movement;
    InputAction fire;
    float xThrow, yThrow;

    // Called before Start, check Unity call flowsheet
    // https://docs.unity3d.com/Manual/ExecutionOrder.html
    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    void Start()
    {
        movement = GetComponent<PlayerInput>().actions["Movement"];
        fire = GetComponent<PlayerInput>().actions["Fire"];
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        Debug.Log(xThrow);
        Debug.Log(yThrow);

        float xOffset = xThrow * xVelocity * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * yVelocity * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(
                clampedXPos,
                clampedYPos,
                transform.localPosition.z
            );
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControllThrow = controlPitchFactor * yThrow;

        float pitch = pitchDueToControllThrow + pitchDueToPosition;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = controlRollFactor * xThrow;
        
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation =
            Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationFactor);
    }

    void ProcessFiring()
    {
        ParticleSystem[] lasers = GetComponentsInChildren<ParticleSystem>();
        if (fire.IsPressed())
        {
            foreach (ParticleSystem laser in lasers)
            {
                if (!laser.isPlaying)
                {
                    laser.Play();
                }
            }

        }
        else
        {
            foreach (ParticleSystem laser in lasers)
            {
                if (laser.isPlaying)
                {
                    laser.Stop();
                }
            }
        }
    }

}
