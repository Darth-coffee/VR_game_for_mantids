using UnityEngine;

/// <summary>
/// Adjusts the camera's horizontal FOV and orientation to align with a specified edge.
/// </summary>
/// <remarks>
/// By Pavan Kumar Kaushik (@pvnkmrksk)
/// https://github.com/pvnkmrksk
/// </remarks>
public class HorizontalCameraAdjuster : MonoBehaviour
{
    [Tooltip("X, Z coordinates of the edge start point")]
    public Vector2 edgeStart = new Vector2(0, 0f);

    [Tooltip("X, Z coordinates of the edge end point")]
    public Vector2 edgeEnd = new Vector2(0, 10f);

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("HorizontalCameraAdjuster must be attached to a GameObject with a Camera component.");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        AdjustCameraToEdge();
    }

    private void AdjustCameraToEdge()
    {
        Vector3 cameraPos = transform.position;
        Vector3 edgeStartPos = new Vector3(edgeStart.x, cameraPos.y, edgeStart.y);
        Vector3 edgeEndPos = new Vector3(edgeEnd.x, cameraPos.y, edgeEnd.y);

        // Calculate vectors from camera to edge points
        Vector3 toEdgeStart = edgeStartPos - cameraPos;
        Vector3 toEdgeEnd = edgeEndPos - cameraPos;

        // Project these vectors onto the horizontal plane (XZ)
        Vector2 toEdgeStartHorizontal = new Vector2(toEdgeStart.x, toEdgeStart.z);
        Vector2 toEdgeEndHorizontal = new Vector2(toEdgeEnd.x, toEdgeEnd.z);

        // Calculate the angle between these horizontal vectors
        float horizontalAngle = Vector2.Angle(toEdgeStartHorizontal, toEdgeEndHorizontal);

        // Clamp the horizontal FOV
        float horizontalFOV = Mathf.Clamp(horizontalAngle, 1f, 179f);

        // Convert horizontal FOV to vertical FOV
        float verticalFOV = Camera.HorizontalToVerticalFieldOfView(horizontalFOV, cam.aspect);

        // Set the camera's vertical FOV
        cam.fieldOfView = verticalFOV;

        // Calculate the direction that bisects the angle in 3D space
        Vector3 bisector = (toEdgeStart.normalized + toEdgeEnd.normalized).normalized;

        // Set the camera's forward direction to this bisector
        transform.forward = bisector;

        Debug.Log($"Horizontal FOV: {horizontalFOV}, Vertical FOV: {verticalFOV}, Rotation: {transform.rotation.eulerAngles}");
    }
}