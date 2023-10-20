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

        renderer = GetComponent<MeshRenderer>();
        outlineMaterial = new Material(outlineShader);
    }

void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            Renderer renderer = hitObject.GetComponent<Renderer>();
            List<Material> materials = new List<Material>(renderer.materials);
            
            if (!materials.Contains(outlineMaterial))
                materials.Add(outlineMaterial);

            renderer.materials = materials.ToArray();
        }
    }
}
}
