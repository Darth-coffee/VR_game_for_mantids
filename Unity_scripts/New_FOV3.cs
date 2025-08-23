using UnityEngine;

public class New_FOV3 : MonoBehaviour
{
    public Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        if (camera == null)
        {
            Debug.LogError("CameraFOVController must be attached to a GameObject with a Camera component.");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        Vector3 cameraPos = transform.position;
        Vector3 edgeStartPos = new Vector3(-350, cameraPos.y, 0);
        Vector3 edgeEndPos = new Vector3(175, cameraPos.y, -310);

        // Calculate vectors from camera to edge points
        Vector3 toEdgeStart = edgeStartPos - cameraPos;
        Vector3 toEdgeEnd = edgeEndPos - cameraPos;

        // Project these vectors onto the horizontal plane (XZ)
        Vector2 toEdgeStartHorizontal = new Vector2(toEdgeStart.x, toEdgeStart.z);
        Vector2 toEdgeEndHorizontal = new Vector2(toEdgeEnd.x, toEdgeEnd.z);

        // Calculate FOV for camera1
        float horizontalFOV = Vector2.Angle(toEdgeStartHorizontal, toEdgeEndHorizontal);

        // Set the FOV for each camera
        camera.fieldOfView = horizontalFOV;

        // Convert horizontal FOV to vertical FOV
        float verticalFOV = Camera.HorizontalToVerticalFieldOfView(horizontalFOV, camera.aspect);

        // Set the camera's vertical FOV
        camera.fieldOfView = verticalFOV;

        // Calculate the direction that bisects the angle in 3D space
        Vector3 bisector = (toEdgeStart.normalized + toEdgeEnd.normalized).normalized;

        // Set the camera's forward direction to this bisector
        transform.forward = bisector;

        Debug.Log($"Horizontal FOV: {horizontalFOV}, Vertical FOV: {verticalFOV}, Rotation: {transform.rotation.eulerAngles}");
    }
}

