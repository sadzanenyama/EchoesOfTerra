using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShieldOffset : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Speed at which the offset changes
    public Renderer targetRenderer;  // Reference to the renderer with the material
    private Material targetMaterial; // Material instance to manipulate

    public float shieldHealth;

    [SerializeField] private SpaceShipManager spaceShipHealth;

    void Start()
    {
     
        // Get the material from the renderer
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        if (targetRenderer != null)
        {
            targetMaterial = targetRenderer.material;
        }
        else
        {
            Debug.LogWarning("No Renderer found on the object.");
        }
    }

    void Update()
    {
        if (targetMaterial == null || spaceShipHealth.shipStats.shieldHealth == 0f) return;

        shieldHealth = spaceShipHealth.currentShield / spaceShipHealth.shipStats.shieldHealth;

        // Apply the offset to the material
        targetMaterial.mainTextureOffset = transform.position;

        float emissionIntensity = Mathf.Lerp(0, -7, 1 - shieldHealth);

        Color emissionColor = Color.cyan * Mathf.Pow(2.0f, emissionIntensity);
        targetMaterial.SetColor("_EmissionColor", emissionColor);

        if (shieldHealth <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<SphereCollider>().enabled = true;
        }
    }
}
