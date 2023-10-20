using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    public Shader outlineShader; // assign in inspector
    public Material outlineMaterial;
    public Renderer renderer;

    void Start()
    {
        if (outlineShader == null)
        {
            Debug.LogError("Outline Shader not assigned.");
            return;
        }

        renderer = GetComponent<Renderer>();
        outlineMaterial = new Material(outlineShader);
    }

    void OnMouseEnter()
    {
        Debug.Log("마우스 올림");
        List<Material> materials = new List<Material>(renderer.materials);
        
        if (!materials.Contains(outlineMaterial))
            materials.Add(outlineMaterial);

        renderer.materials = materials.ToArray();
    }

    void OnMouseExit()
    {
        Debug.Log("마우스 뺌");

        List<Material> materials = new List<Material>(renderer.materials);

        if (materials.Contains(outlineMaterial))
            materials.Remove(outlineMaterial);

        renderer.materials = materials.ToArray();
    }
}
