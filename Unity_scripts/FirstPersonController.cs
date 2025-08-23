using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public Transform playerCamera;

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle mouse look (horizontal rotation only)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // Rotate the player around the Y-axis only
        transform.Rotate(Vector3.up * mouseX);

        // Handle movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        transform.position += move * speed * Time.deltaTime;
    }
}
