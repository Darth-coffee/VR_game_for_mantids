using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Reference to the UDPReceive component
    public UDPReceive udpReceive;

    void Start()
    {
        // Initialization if needed
    }

    void Update()
    {
        // Get the data from the UDP receiver
        string data = udpReceive.data;

        // Check if data is empty or null
        if (string.IsNullOrEmpty(data))
        {
            Debug.LogError("No data received from UDP.");
            return;
        }

        // Clean up the data by removing the first and last characters
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);

        // Example data format: (255,361,50012)
        string[] info = data.Split(',');

        // Check if we have the expected number of elements
        if (info.Length < 2)
        {
            Debug.LogError("Received data does not contain expected number of elements.");
            return;
        }

        // Parse the data to get x, y, and z values
        float x = (float.Parse(info[0])); // Increase sensitivity by multiplying by 2
        float z = -(float.Parse(info[1])); // Increase sensitivity by multiplying by 2

        // Log the parsed values to debug
        Debug.Log($"Parsed Values - x: {x}, z: {z}");

        // Update the position of the game object
        gameObject.transform.localPosition = new Vector3(x, 100, z);
    }
}
