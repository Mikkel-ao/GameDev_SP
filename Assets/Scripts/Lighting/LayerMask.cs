using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LayerMask : MonoBehaviour
{
    public uint renderingLayerMask = 1; // 1 = Layer 1, 2 = Layer 2, etc.

    [ContextMenu("Apply Mask to All Children")]
    void ApplyMask()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var rend in renderers)
        {
            rend.renderingLayerMask = renderingLayerMask;
        }

        Debug.Log($"Applied mask {renderingLayerMask} to {renderers.Length} child MeshRenderers.");
    }
}