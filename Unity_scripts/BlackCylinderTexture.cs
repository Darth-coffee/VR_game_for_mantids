using UnityEngine;

public class BlackCylinderTexture : MonoBehaviour
{
    // Reference to the cylinder object
    public GameObject cylinder;

    // Start is called before the first frame update
    void Start()
    {
        if (cylinder == null)
        {
            Debug.LogError("Cylinder object is not assigned.");
            return;
        }

        // Create a new material with the Standard shader
        Material blackMaterial = new Material(Shader.Find("Standard"));
        
        // Set the color to black
        blackMaterial.color = Color.black;

        // Ensure the material responds to lighting and is rough
        blackMaterial.SetFloat("_Glossiness", 0.2f);  // For a rough, matte look
        blackMaterial.SetFloat("_Metallic", 0.0f);    // Non-metallic

        // Apply the material to the cylinder
        Renderer renderer = cylinder.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = blackMaterial;
        }
        else
        {
            Debug.LogError("No Renderer found on the cylinder object.");
        }
    }
}
