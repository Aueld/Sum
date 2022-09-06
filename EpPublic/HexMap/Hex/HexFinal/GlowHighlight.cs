using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowHighlight : MonoBehaviour
{
    Dictionary<Renderer, Material[]> originalMaterialDictionary = new Dictionary<Renderer, Material[]>();

    private bool isGlowing = false;

    private void Awake()
    {
        PrepareMaterialDictionaries();
    }

    private void PrepareMaterialDictionaries()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material[] originMaterials = renderer.materials;
            originalMaterialDictionary.Add(renderer, originMaterials);
        }
    }

    public void ToggleGlow()
    {
        if (isGlowing == false)
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials[0].SetFloat("_ToggleSwitch0", 1);
            }
        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials[0].SetFloat("_BlinkTime", 5f);
                renderer.materials[0].SetFloat("_ToggleSwitch0", 0);
            }
        }
        isGlowing = !isGlowing;
    }

    public void ToggleGlow(bool state)
    {
        if (isGlowing == state)
            return;
        isGlowing = !state;
        ToggleGlow();
    }

}
