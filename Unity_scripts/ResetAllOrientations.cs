using UnityEngine;

public class ResetAllOrientations : MonoBehaviour
{
    // Store the initial rotations of all GameObjects
    private Quaternion[] initialRotations;
    private GameObject[] allGameObjects;

    void Start()
    {
        // Find all GameObjects in the scene
        allGameObjects = FindObjectsOfType<GameObject>();

        // Initialize the initialRotations array
        initialRotations = new Quaternion[allGameObjects.Length];

        // Save the initial rotation of each GameObject
        for (int i = 0; i < allGameObjects.Length; i++)
        {
            initialRotations[i] = allGameObjects[i].transform.rotation;
        }
    }

    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Reset the rotation of each GameObject
            for (int i = 0; i < allGameObjects.Length; i++)
            {
                allGameObjects[i].transform.rotation = initialRotations[i];
            }
        }
    }
}
