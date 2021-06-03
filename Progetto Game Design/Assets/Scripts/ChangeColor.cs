using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    List<Color> originalColors = new List<Color>();

    private int originalColorIndex;

    private void Start()
    {
        // Find all the children of the GameObject with MeshRenderers

        SkinnedMeshRenderer[] children = GetComponentsInChildren<SkinnedMeshRenderer>();

        // Cycle through each child object found with a MeshRenderer

        foreach (SkinnedMeshRenderer rend in children)
        {
            // And for each child, cycle through each material

            foreach (Material mat in rend.materials)
            {
                // Store original colors

                originalColors.Add(mat.color);
            }
        }
    }

    void Update()
    {
        // StartCoroutine to flash GameObject
        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine("HitFlash");
        }
    }

    public IEnumerator HitFlash()
    {
        // Flash color

        SkinnedMeshRenderer[] children = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer rend in children)
        {
            foreach (Material mat in rend.materials)
            {
                mat.SetColor("_Color", Color.white);
            }
        }

        yield return new WaitForSeconds(0.1f);

        // Restore colors

        foreach (SkinnedMeshRenderer rend in children)
        {
            foreach (Material mat in rend.materials)
            {
                mat.SetColor("_Color", originalColors[originalColorIndex]);

                // Increment originalColorIndex by 1

                originalColorIndex += 1;
            }
        }

        // Reset originalColorIndex

        originalColorIndex = 0;

        StopCoroutine("HitFlash");
    }
}