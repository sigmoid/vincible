using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegateEffect : MonoBehaviour
{
    public Material EffectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (EffectMaterial != null)
        {
            Graphics.Blit(src, dest, EffectMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
