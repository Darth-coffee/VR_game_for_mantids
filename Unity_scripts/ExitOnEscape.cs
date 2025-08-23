using UnityEngine;

public class ExitOnEscape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            // Note: This will not work in the editor. Use UnityEditor.EditorApplication.isPlaying = false to stop play mode in the editor.
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}

