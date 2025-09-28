//using System.IO.Ports;
using UnityEngine;

public class SensorControls : MonoBehaviour
{
    // Dead zone for yaw (twist)
    public float yawDeadZone = 0.5f;

    // Movement Speeds
    public float forwardSpeed = 40f;
    public float strafeSpeed = 7.5f;
    public float hoverSpeed = 5f;
    public float pitchSpeed = 90f; // Added pitch speed

    // Movement Acceleration
    private float activeForwardSpeed;
    private float activeRollSpeed, activePitchSpeed; // Added activePitchSpeed
    private float forwardAcceleration = 2.5f;
    private float rollAcceleration = 3.5f, pitchAcceleration = 3.5f; // Added pitchAcceleration

    // Rotation Speeds and Sensitivity
    public float rollSpeed = 90f;

    // Sensor-based control sensitivity
    public float thrustSensitivity = 0.5f;
    public float yawSensitivity = 0.8f;
    public float rollSensitivity = 0.8f;
    public float pitchSensitivity = 0.8f;

    // References to the sensor data scripts
    private RightHand rightHand;
    private LeftHand leftHand;

    void Start()
    {
        // Get the singleton instances of the sensor data scripts
        rightHand = RightHand.instance;
        leftHand = LeftHand.instance;

        // Lock the cursor for in-game control
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check if both sensor scripts are available before proceeding
        if (rightHand == null || leftHand == null)
        {
            Debug.LogWarning("Sensor data scripts are not assigned. Ship control will be inactive.");
            return;
        }

        // --- Handle Ship Movement ---
        // Thrust (Forward/Backward)
        float verticalInput = rightHand.AccelY * thrustSensitivity;
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, verticalInput * forwardSpeed, forwardAcceleration * Time.deltaTime);

        // Roll (Side-to-Side)
        float rollInput = leftHand.AccelX * rollSensitivity;
        activeRollSpeed = Mathf.Lerp(activeRollSpeed, rollInput * rollSpeed, rollAcceleration * Time.deltaTime);

        // Pitch (Up/Down)
        float pitchInput = leftHand.AccelY * pitchSensitivity;
        activePitchSpeed = Mathf.Lerp(activePitchSpeed, pitchInput * pitchSpeed, pitchAcceleration * Time.deltaTime);

        transform.Rotate(
            -activeRollSpeed * Time.deltaTime, // Use -activePitchSpeed to match hand tilt
            0, // Yaw is not implemented in this code snippet
            -activePitchSpeed * Time.deltaTime,  // Use -activeRollSpeed to match hand tilt
            Space.Self
        );

        transform.position += activeForwardSpeed * Time.deltaTime * transform.forward;

    }
}