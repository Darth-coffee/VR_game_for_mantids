using UnityEngine;

public class MultiDisplayActivator : MonoBehaviour
{
    void Start()
    {
        // Activate additional displays if available
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
        if (Display.displays.Length > 2)
        {
            Display.displays[2].Activate();
        }
        if (Display.displays.Length > 3)
        {
            Display.displays[3].Activate();
        }

        // Log the number of connected displays
        Debug.Log("Number of connected displays: " + Display.displays.Length);
    }
}
