using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using System.Text;
using System;

public class BlobDataReceiver : MonoBehaviour
{
    public string ipAddress = "127.0.0.1";  // IP address where Python script sends data
    public int port = 5555;  // Port number used for ZeroMQ communication
    public Transform playerTransform;  // Reference to the player's transform

    private SubscriberSocket subscriber;
    private bool isListening = false;

    void Start()
    {
        // Initialize ZeroMQ subscriber socket
        subscriber = new SubscriberSocket();
        subscriber.Connect($"tcp://{ipAddress}:{port}");
        subscriber.Subscribe("BlobData");  // Subscribe to the topic "BlobData"

        // Start listening for messages
        isListening = true;
        StartListening();
    }

    void StartListening()
    {
        // Start a new thread for listening to ZeroMQ messages
        System.Threading.Thread thread = new System.Threading.Thread(() =>
        {
            while (isListening)
            {
                try
                {
                    // Receive message from ZeroMQ socket
                    string message = subscriber.ReceiveFrameString();
                    HandleMessage(message);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error receiving data: " + e.Message);
                }
            }
        });
        thread.Start();
    }

    void HandleMessage(string message)
    {
        // Parse the received message
        string[] parts = message.Split(',');
        if (parts.Length >= 6)
        {
            float x1 = float.Parse(parts[0]);
            float y1 = float.Parse(parts[1]);
            float x2 = float.Parse(parts[2]);
            float y2 = float.Parse(parts[3]);
            float angleDeg = float.Parse(parts[4]);
            float angleSin = float.Parse(parts[5]);

            // Calculate translation vector based on blob positions
            Vector3 translation = new Vector3((x1 + x2) / 2f, 0f, (y1 + y2) / 2f);

            // Set player's position
            playerTransform.position = translation;

            // Calculate rotation based on angle between blobs
            Quaternion rotation = Quaternion.Euler(0f, angleDeg, 0f);

            // Set player's rotation
            playerTransform.rotation = rotation;
        }
    }

    void OnDestroy()
    {
        // Stop listening and clean up resources when script is destroyed
        isListening = false;
        subscriber?.Close();
    }
}
