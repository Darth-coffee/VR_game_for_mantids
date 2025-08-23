using UnityEngine;

public class OscillateCylinderRandom : MonoBehaviour
{
    public float amplitude = 1.0f; // Amplitude of oscillation
    public float minSpeed = 0.5f; // Minimum speed of oscillation
    public float maxSpeed = 2.0f; // Maximum speed of oscillation

    private float currentSpeed; // Current speed of oscillation
    private int oscillationCount = 0; // Counter for oscillations
    private bool isOscillating = false; // Flag to check if oscillation is active
    private float timeSinceSpeedChange = 0f; // Time since last speed change
    private float previousSinValue = 0f; // Previous value of the sine function

    private Vector3 startPosition; // Starting position of the cylinder

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Check if the space key is pressed to toggle oscillation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOscillating = !isOscillating;
            if (isOscillating)
            {
                ChangeSpeed();
            }
        }

        if (isOscillating)
        {
            timeSinceSpeedChange += Time.deltaTime;
            float x = startPosition.x + amplitude * Mathf.Sin(currentSpeed * timeSinceSpeedChange);
            transform.position = new Vector3(x, startPosition.y, startPosition.z);

            // Check for complete oscillations and update speed every 2 oscillations
            float currentSinValue = Mathf.Sin(currentSpeed * timeSinceSpeedChange);
            if (previousSinValue <= 0 && currentSinValue > 0)
            {
                oscillationCount++;
                if (oscillationCount % 2 == 0)
                {
                    ChangeSpeed();
                }
            }
            previousSinValue = currentSinValue;
        }
    }

    void ChangeSpeed()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        timeSinceSpeedChange = 0f; // Reset the time since last speed change
        previousSinValue = 0f; // Reset the previous sine value to handle oscillation count correctly
    }
}
