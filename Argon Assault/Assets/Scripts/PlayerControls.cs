using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves left to right")] [SerializeField] float xVelocity = 25f;
    [Tooltip("How fast ship moves up to down")] [SerializeField] float yVelocity = 25f;
    [Tooltip("How far the ship can move horizontally")] [SerializeField] float xRange = 10f;
    [Tooltip("How far the ship can move vertically")] [SerializeField] float yRange = 7f;
    [Tooltip("How quickly the ship rotates while moving")] [SerializeField] float rotationFactor = 0.7f;
    [Header("Screen-position Based Tuning")]
    [SerializeField] float positionPitchFactor = -1f;
    [SerializeField] float positionYawFactor = 2f;
    [Header("Player input Based Tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;
    [Header("Laser array")]
    [Tooltip("Add all player lasers here")] [SerializeField] GameObject[] lasers;

    InputAction movement;
    InputAction fire;
    float xThrow, yThrow;

    // Called before Start, check Unity call flowsheet
    // https://docs.unity3d.com/Manual/ExecutionOrder.html

    void Start()
    {
        // Get the input action from the PlayerInput component
        // The PlayerInput component is an Input Action Asset that is modified in the Unity editor itself
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
        if (fire.IsPressed())
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);

        }
    }
    
    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
